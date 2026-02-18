using Microsoft.Extensions.Options;
using StackExchange.Redis;
using User.Interfaces;
using User.Services.Jwt;

namespace User.Services.Authentication;

public class RedisRefreshTokenStore(IConnectionMultiplexer redis, IOptions<JwtSettings> options) : IRefreshTokenStore
{

    private readonly IDatabase _db = redis.GetDatabase();
    private readonly JwtSettings _option = options.Value;

    // private readonly IDatabase _db = redis.GetDatabase();
    // private readonly JwtSettings _option = options.Value;    
    // public async Task<string?> GetAsync(int userId)
    // {
    //     return await _db.StringGetAsync(ReturnRefreshTokenKey(userId));
    // }

    // public async Task RemoveAsync(int userId)
    // {
    //     await _db.KeyDeleteAsync(ReturnRefreshTokenKey(userId));
    // }

    // public async Task SaveAsync(int userId, string refreshTokenHash)
    // {
    //     TimeSpan expiry = TimeSpan.FromDays(_option.RefreshTokenExpiryDays);
    //     await _db.StringSetAsync(ReturnRefreshTokenKey(userId), refreshTokenHash, expiry);
    // }

    // private static string ReturnRefreshTokenKey(int userId) => $"refresh:{userId}";
    public async Task<int?> GetUserIdAsync(string refreshTokenHash)
    {
        var value = await _db.StringGetAsync(ReturnKey(refreshTokenHash));
        return value.IsNull ? null : int.Parse(value!);
    }

    public async Task SaveAsync(int userId, string refreshTokenHash)
    {
        var expiry = TimeSpan.FromDays(_option.RefreshTokenExpiryDays);

        await _db.StringSetAsync(
            ReturnKey(refreshTokenHash),
            userId,
            expiry
        );
    }

    public async Task RemoveAsync(string refreshTokenHash)
    {
        await _db.KeyDeleteAsync(ReturnKey(refreshTokenHash));
    }

    private static string ReturnKey(string hash)
        => $"refresh:{hash}";
}
