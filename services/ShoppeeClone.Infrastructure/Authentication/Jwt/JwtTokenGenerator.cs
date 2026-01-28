namespace ShoppeeClone.Infrastructure.Authentication.Jwt;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppeeClone.Application.Common.Interfaces;

public class JwtTokenGenerator(IOptions<JwtSettings> options) : IJwtTokenGenerator
{
    private readonly JwtSettings _options = options.Value;
    public string GenerateToken(int userId, string firstName, string lastName, string email)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecreteKey)),
            SecurityAlgorithms.HmacSha256
        );
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.GivenName, firstName),
            new(JwtRegisteredClaimNames.FamilyName, lastName),
            new(JwtRegisteredClaimNames.Email, email)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            expires: DateTime.Now.AddDays(_options.ExpiryDays),
            claims: claims,
            signingCredentials: signingCredentials,
            audience: _options.Audience
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}