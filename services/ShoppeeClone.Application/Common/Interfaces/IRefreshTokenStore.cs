namespace ShoppeeClone.Application.Common.Interfaces;

public interface IRefreshTokenStore
{
    Task SaveAsync(int userId, string refreshTokenHash, TimeSpan expiry);
    Task<string?> GetAsync(int userId);
    Task RemoveAsync(int userId);

}