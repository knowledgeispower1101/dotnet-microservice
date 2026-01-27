using ShoppeeClone.Application.Common.Exceptions;
using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Application.Common.Interfaces.Security;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<AuthenticationResult> Login(string email, string password)
    {
        if (await _userRepository.GetUserByEmail(email) is not User user || user.Password != password)
        {
            throw new Exception("Email or Password is not correct");
        }
        //string userId, string firstName, string lastName, string[] roles, string email
        string token = _jwtTokenGenerator.GenerateToken(user.Id, "firstName", "lastName", email);
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

    public async Task<User> Register(string firstName, string lastName, string email, string password)
    {
        if (await _userRepository.GetUserByEmail(email) is User userDB) throw new UserAlreadyExistsException(email);
        string hashedPassword = _passwordHasher.Hash(password);
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = hashedPassword
        };
        return await _userRepository.Add(user);
    }
}