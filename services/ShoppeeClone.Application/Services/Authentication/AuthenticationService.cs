using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    public AuthenticationResult Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user || user.Password != password)
        {
            throw new Exception("Email or Password is not correct");
        }
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
            token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null) throw new Exception($"User with this {email} is already exists.");
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
        };
        return new AuthenticationResult(
                   "userId",
                   firstName,
                   lastName,
                   email,
                   ""
               );
    }

}