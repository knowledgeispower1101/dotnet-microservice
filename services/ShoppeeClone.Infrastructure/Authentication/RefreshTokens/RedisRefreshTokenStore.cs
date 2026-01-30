namespace ShoppeeClone.Infrastructure.Authentication.RefreshTokens;

using ShoppeeClone.Application.Common.Interfaces;
using StackExchange.Redis;

public class RedisRefreshTokenStore(IConnectionMultiplexer redis) : IRefreshTokenStore
{
    private readonly IDatabase _db = redis.GetDatabase();

    public async Task<string?> GetAsync(int userId)
    {
        return await _db.StringGetAsync($"refresh:{userId}");
    }

    public async Task RemoveAsync(int userId)
    {
        await _db.KeyDeleteAsync($"refresh:{userId}");
    }

    public async Task SaveAsync(int userId, string refreshTokenHash, TimeSpan expiry)
    {
        await _db.StringSetAsync($"refresh:{userId}", refreshTokenHash, expiry);
    }
}
