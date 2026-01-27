namespace ShoppeeClone.Infrastructure.Authentication.Jwt;

public class JwtSettings
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpiryDays { get; init; }
    public string SecreteKey { get; init; } = null!;
}
