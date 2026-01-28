namespace ShoppeeClone.Infrastructure.Authentication.RefreshTokens;

public class RedisSettings
{
    public string Connection { get; init; } = null!;
    public string InstanceName { get; init; } = null!;
}