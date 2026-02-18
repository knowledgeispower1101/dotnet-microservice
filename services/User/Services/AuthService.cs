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
    private static readonly string GUEST = "guest";
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRefreshTokens _refreshTokens = refreshTokens;
    private readonly IRefreshTokenStore _refreshTokenStore = refreshTokenStore;
    private readonly AppDbContext _context = dbContext;
    public async Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        // Commented out original logic - keeping the mock response for now
        // var user = await _userRepository.GetUserByEmail(email);
        // if (user is null || !_passwordHasher.Verify(password, user.PasswordHash)) 
        //     throw new WrongEmailPasswordException();

        // string refreshToken = _refreshTokens.Generate();
        // await _refreshTokenStore.SaveAsync(user.Id, refreshToken);

        // var accessToken = _jwtTokenGenerator.GenerateToken(
        //     user.Id,
        //     user.FirstName,
        //     user.LastName,
        //     user.Email
        // );

        LoginResponse loginResponse = new(
            1,
            "query.Email",
            "accessToken",
            "refreshToken",
            "user.LastName",
            "user.FirstName"
        );

        return BaseResponse<LoginResponse>.Ok(loginResponse, "Login successfully");
    }

    public async Task<BaseResponse<string>> LogoutAsync(Guid userId)
    {
        // TODO: Get refresh token hash from userId or pass it as parameter
        // For now, this is a placeholder implementation
        // await _refreshTokenStore.RemoveAsync(refreshTokenHash);
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
                .FirstOrDefaultAsync(r => r.RoleName.Equals(GUEST, StringComparison.CurrentCultureIgnoreCase))
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




}
