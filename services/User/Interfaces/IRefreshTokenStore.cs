namespace User.Interfaces;

public interface IRefreshTokenStore
{
    Task<Guid?> GetUserIdAsync(string refreshTokenHash);
    Task SaveAsync(Guid userId, string refreshTokenHash);
    Task RemoveAsync(string refreshTokenHash);
}
