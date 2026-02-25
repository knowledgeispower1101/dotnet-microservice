using Microsoft.Extensions.Options;
using StackExchange.Redis;
using User.Interfaces;
using User.Services.Jwt;

namespace User.Services.RefreshTokens;

public class RedisRefreshTokenStore(IConnectionMultiplexer redis, IOptions<JwtSettings> options) : IRefreshTokenStore
{
    private readonly IDatabase _db = redis.GetDatabase();
    private readonly JwtSettings _option = options.Value;

    public async Task<Guid?> GetUserIdAsync(string refreshTokenHash)
    {
        var value = await _db.StringGetAsync(ModifyKeyRefreshToken(refreshTokenHash));
        if (value.IsNull) return null;
        return Guid.TryParse(value, out var guid) ? guid : null;
    }

    public async Task SaveAsync(Guid userId, string refreshTokenHash)
    {
        var expiry = TimeSpan.FromDays(_option.RefreshTokenExpiryDays);

        var refreshKey = ModifyKeyRefreshToken(refreshTokenHash);
        var userKey = ModifyKeyUser(userId);

        var tran = _db.CreateTransaction();

        _ = tran.StringSetAsync(refreshKey, userId.ToString(), expiry);

        _ = tran.SetAddAsync(userKey, refreshTokenHash);

        _ = tran.KeyExpireAsync(userKey, expiry);

        await tran.ExecuteAsync();
    }


    public async Task RemoveAsync(string refreshTokenHash)
    {
        var refreshKey = ModifyKeyRefreshToken(refreshTokenHash);
        var userIdValue = await _db.StringGetAsync(refreshKey);
        if (userIdValue.IsNull) return;

        var userId = Guid.Parse(userIdValue!);
        var userKey = ModifyKeyUser(userId);

        var tran = _db.CreateTransaction();

        _ = tran.KeyDeleteAsync(refreshKey);

        _ = tran.SetRemoveAsync(userKey, refreshTokenHash);

        await tran.ExecuteAsync();
    }

    private static string ModifyKeyRefreshToken(string key) => $"refresh:{key}";
    private static string ModifyKeyUser(Guid userId) => $"user:{userId}";
}
