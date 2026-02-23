using Microsoft.Extensions.Options;
using StackExchange.Redis;
using User.Interfaces;
using User.Services.Jwt;

namespace User.Services.Authentication;

public class RedisRefreshTokenStore(IConnectionMultiplexer redis, IOptions<JwtSettings> options) : IRefreshTokenStore
{
    private readonly IDatabase _db = redis.GetDatabase();
    private readonly JwtSettings _option = options.Value;

    public async Task<Guid?> GetUserIdAsync(string refreshTokenHash)
    {
        var value = await _db.StringGetAsync(ReturnKey(refreshTokenHash));
        if (value.IsNull) return null;
        return Guid.TryParse(value, out var guid) ? guid : null;
    }

    public async Task SaveAsync(Guid userId, string refreshTokenHash)
    {
        var expiry = TimeSpan.FromDays(_option.RefreshTokenExpiryDays);

        await _db.StringSetAsync(
            ReturnKey(refreshTokenHash),
            userId.ToString(),
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
