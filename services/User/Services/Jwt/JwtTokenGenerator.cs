using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Interfaces;

namespace User.Services.Jwt;

public class JwtTokenGenerator(IOptions<JwtSettings> options) : IJwtTokenGenerator
{
    private readonly JwtSettings _options = options.Value;

    public string GenerateToken(Guid userId, string firstName, string lastName, string email, string username, string[] listRole)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.GivenName, firstName),
            new(JwtRegisteredClaimNames.FamilyName, lastName),
            new(JwtRegisteredClaimNames.PreferredUsername, username),
        };

        foreach (var role in listRole)
        {
            claims.Add(new Claim("role", role));
        }

        var securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            expires: DateTime.Now.AddMinutes(_options.ExpiryDays),
            claims: claims,
            signingCredentials: signingCredentials,
            audience: _options.Audience
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }
}