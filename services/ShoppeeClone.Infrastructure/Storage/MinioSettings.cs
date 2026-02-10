namespace ShoppeeClone.Infrastructure.Storage;

public class MinioSettings
{
    public string EndPoint { get; init; } = null!;
    public string AccessKey { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public bool UseSSL { get; init; } = false;
}