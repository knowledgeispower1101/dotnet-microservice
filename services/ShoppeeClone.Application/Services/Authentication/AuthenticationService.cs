using ShoppeeClone.Application.Common.Exceptions;
using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Application.Common.Interfaces.Authentication.Password;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Application.Services.Authentication;

public class AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher, IRefreshTokens refreshTokens) : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRefreshTokens _refreshTokens = refreshTokens;
    public async Task<AuthenticationResult> Login(string email, string password)
    {
        //string userId, string firstName, string lastName, string[] roles, string email
        // check email and password
        var user = await _userRepository.GetUserByEmail(email);
        if (user is null || !_passwordHasher.Verify(password, user.Password)) throw new Exception("Email or Password is not correct");
        // get information from email
        // pass to authenticatio result
        // generate refresh token

        // string refreshToken
        string refreshToken = _refreshTokens.Generate();
        // string accessToken
        string accessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName, email);
        // save refresh token to redis
        return new AuthenticationResult(
            user.Id,
            email,
            accessToken,
            user.LastName,
            user.FirstName
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