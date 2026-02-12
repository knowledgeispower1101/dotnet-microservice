namespace Shared.Infrastructure.Cache;

public class RedisSettings
{
    public string Connection { get; set; } = default!;
    public string InstanceName { get; set; } = default!;
}
