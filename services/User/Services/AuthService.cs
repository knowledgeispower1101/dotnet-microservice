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
    AppDbContext dbContext) : IAuthService
{
    private static readonly string GUEST = "GUEST";
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRefreshTokens _refreshTokens = refreshTokens;
    private readonly IRefreshTokenStore _refreshTokenStore = refreshTokenStore;
    private readonly AppDbContext _context = dbContext;

    public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _context.UsersApp
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == request.Email)
            ?? throw new UnauthorizedException("Invalid email or password");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password");

        string accessToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName ?? string.Empty,
            user.LastName ?? string.Empty,
            user.Email
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
        string? token = null;

        if (request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var value = authHeader.ToString();
            if (value.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = value["Bearer ".Length..].Trim();
        }
        else if (request.Headers.TryGetValue("accessToken", out var rawToken))
        {
            token = rawToken.ToString();
        }

        if (string.IsNullOrWhiteSpace(token))
            throw new BadRequestException("Missing or malformed token");

        bool isValid = _jwtTokenGenerator.ValidateToken(token);
        return Task.FromResult(isValid);
    }
}

