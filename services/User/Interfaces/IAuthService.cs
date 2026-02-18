using Shared.Application.Common.Response;

namespace User.Interfaces;

public interface IAuthService
{
    Task<BaseResponse<LoginResponse>> LoginAsync(string email, string password);
    Task<BaseResponse<string>> RegisterAsync(string username, string email, string password, string firstName, string lastName, string phoneNumber);
    Task<BaseResponse<string>> LogoutAsync(Guid userId);
}

public record LoginResponse(
    int Id,
    string Email,
    string AccessToken,
    string RefreshToken,
    string LastName,
    string FirstName
);
