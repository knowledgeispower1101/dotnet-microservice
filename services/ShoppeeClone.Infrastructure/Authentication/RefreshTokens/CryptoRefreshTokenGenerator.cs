using System.Security.Cryptography;
using ShoppeeClone.Application.Common.Interfaces;

namespace ShoppeeClone.Infrastructure.Authentication.RefreshTokens;

public class CryptoRefreshTokenGenerator : IRefreshTokens
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
