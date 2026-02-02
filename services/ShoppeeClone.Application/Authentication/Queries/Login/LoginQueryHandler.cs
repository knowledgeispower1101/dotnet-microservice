
using MediatR;
using ShoppeeClone.Application.Common.Errors;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;
using ShoppeeClone.Application.Services.Persistence;

namespace ShoppeeClone.Application.Authentication.Queries.Login;

public class LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IRefreshTokens refreshTokens,
    IRefreshTokenStore refreshTokenStore
    ) : IRequestHandler<LoginQuery, BaseResponse<LoginResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IRefreshTokens _refreshTokens = refreshTokens;
    private readonly IRefreshTokenStore _refreshTokenStore = refreshTokenStore;
    public async Task<BaseResponse<LoginResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetUserByEmail(query.Email);
        if (user is null || !_passwordHasher.Verify(query.Password, user.Password)) throw new WrongEmailPasswordException();
        string refreshToken = _refreshTokens.Generate();
        await _refreshTokenStore.SaveAsync(
            user.Id,
            refreshToken,
            TimeSpan.FromDays(7)
        );
        var accessToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email
        );
        LoginResponse loginResponse = new(user.Id,
                   query.Email,
                   accessToken,
                   refreshToken,
                   user.LastName,
                   user.FirstName);
        return BaseResponse<LoginResponse>.Ok(loginResponse, "Login successfully");
    }

}