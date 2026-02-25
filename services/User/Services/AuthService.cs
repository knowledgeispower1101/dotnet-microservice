using Shared.Application.Common.Exceptions;
using Shared.Application.Common.Response;
using User.DTOs;
using User.Interfaces;
using User.Models;
using Microsoft.EntityFrameworkCore;
using User.Data;

namespace User.Services;

public class AuthService(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IRefreshTokens refreshTokens,
    IRefreshTokenStore refreshTokenStore,
    ILogger<AuthService> logger,
    AppDbContext dbContext) : IAuthService
{
    private static readonly string GUEST = "GUEST";
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRefreshTokens _refreshTokens = refreshTokens;
    private readonly IRefreshTokenStore _refreshTokenStore = refreshTokenStore;
    private readonly AppDbContext _context = dbContext;
    private readonly ILogger<AuthService> _logger = logger;
    public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _context.UsersApp
                .AsNoTracking()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.Email == request.Email)
                .Select(u => new AuthUserWithRoleFlatDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    RoleName = u.UserRoles.Select(ur => ur.Role.RoleName).ToArray()
                })
                .FirstOrDefaultAsync() ?? throw new UnauthorizedException("Invalid email or password");
        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password");

        string accessToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName ?? string.Empty,
            user.LastName ?? string.Empty,
            user.Email,
            user.Username,
            user.RoleName
        );

        string refreshToken = _refreshTokens.Generate();
        await _refreshTokenStore.SaveAsync(user.Id, refreshToken);

        var loginResponse = new LoginResponse(
            user.Id,
            user.Email,
            accessToken,
            refreshToken,
            user.LastName ?? string.Empty,
            user.FirstName ?? string.Empty
        );
        return BaseResponse<LoginResponse>.Ok(loginResponse, "Login successfully");
    }

    public async Task<BaseResponse<string>> LogoutAsync(string refreshToken)
    {
        await _refreshTokenStore.RemoveAsync(refreshToken);
        return BaseResponse<string>.Ok("Logout successfully", "Logout success");
    }

    public async Task<BaseResponse<string>> UserRegisterAsync(RegisterRequest request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (await _context.UsersApp
                .AsNoTracking()
                .AnyAsync(x => x.Email == request.Email))
                throw new DuplicateEmailException(request.Email);

            string hashedPassword = _passwordHasher.Hash(request.Password);

            var user = new UsersApp(
                request.FirstName,
                request.LastName,
                request.Email,
                hashedPassword,
                request.PhoneNumber,
                request.UserName
            );

            Role guest = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RoleName == GUEST)
                ?? throw new BadRequestException("Guest role not found");

            user.UserProfile = new UserProfile(user.Id);

            var userRole = new UserRole(user.Id, guest.Id);
            user.UserRoles.Add(userRole);

            _context.UsersApp.Add(user);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return BaseResponse<string>.Ok(
                "Your account has been successfully created",
                "Register success");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public Task<bool> VerifyToken(HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Authorization", out var authHeader))
            throw new BadRequestException("Missing Authorization header");

        var value = authHeader.ToString();

        if (!value.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            throw new BadRequestException("Invalid Authorization format");

        var token = value["Bearer ".Length..].Trim();

        if (string.IsNullOrWhiteSpace(token)) throw new BadRequestException("Empty token");

        bool isValid = _jwtTokenGenerator.ValidateToken(token);
        return Task.FromResult(isValid);
    }
}

