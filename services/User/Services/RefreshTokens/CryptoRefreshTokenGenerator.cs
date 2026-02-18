using System.Security.Cryptography;
using User.Interfaces;

namespace User.Services.Authentication;

public class CryptoRefreshTokenGenerator : IRefreshTokens
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
