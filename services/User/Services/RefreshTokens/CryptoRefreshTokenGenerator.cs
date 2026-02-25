using System.Security.Cryptography;
using User.Interfaces;

namespace User.Services.RefreshTokens;

public class CryptoRefreshTokenGenerator : IRefreshTokens
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
