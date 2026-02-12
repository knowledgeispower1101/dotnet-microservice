using System.Security.Cryptography;
using User.Application.Authentication.Persistence;

namespace User.Infrastructure.Authentication.RefreshTokens;

public class CryptoRefreshTokenGenerator : IRefreshTokens
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
