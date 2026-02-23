using Shared.Application.Common.Response;
using User.DTOs;

namespace User.Interfaces;

public interface IAuthService
{
    Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request);
    Task<BaseResponse<string>> UserRegisterAsync(RegisterRequest request);
    Task<BaseResponse<string>> LogoutAsync(string refreshToken);
    Task<bool> VerifyToken(HttpRequest request);
}

public record LoginResponse(
    Guid Id,
    string Email,
    string AccessToken,
    string RefreshToken,
    string LastName,
    string FirstName
);
