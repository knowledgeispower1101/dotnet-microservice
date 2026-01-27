namespace ShoppeeClone.Application.Common.Interfaces.Authentication;

public interface IRefreshTokenStore
{
    Task SaveAsync(Guid userId, string refreshTokenHash, TimeSpan expiry);
    Task<string?> GetAsync(Guid userId);
    Task RemoveAsync(Guid userId);

}