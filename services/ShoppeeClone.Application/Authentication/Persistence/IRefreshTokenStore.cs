namespace ShoppeeClone.Application.Authentication.Persistence;

public interface IRefreshTokenStore
{
    Task<int?> GetUserIdAsync(string refreshTokenHash);
    Task SaveAsync(int userId, string refreshTokenHash);
    Task RemoveAsync(string refreshTokenHash);
}
