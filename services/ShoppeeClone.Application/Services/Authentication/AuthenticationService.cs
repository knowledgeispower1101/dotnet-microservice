using ShoppeeClone.Application.Common.Interfaces.Authentication;

namespace ShoppeeClone.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    public AuthenticationResult Login(string email, string password)
    {
        //string userId, string firstName, string lastName, string[] roles, string email
        string token = _jwtTokenGenerator.GenerateToken("userId will get by userservice", "firstName", "lastName", email);
        // check email and password
        // get information from email
        // pass to authenticatio result
        return new AuthenticationResult(
            "userId",
            "firstName",
            "lastName",
            email,
            "token"
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        return new AuthenticationResult(
                   "userId",
                   firstName,
                   lastName,
                   email,
                   "token"
               );
    }

}