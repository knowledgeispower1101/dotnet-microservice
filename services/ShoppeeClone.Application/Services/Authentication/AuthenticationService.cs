using ShoppeeClone.Application.Common.Interfaces.Authentication;

namespace ShoppeeClone.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            email,
            "token"
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        string token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), firstName, lastName, [], email);
        return new AuthenticationResult(
                   Guid.NewGuid(),
                   firstName,
                   lastName,
                   email,
                   token
               );
    }
}