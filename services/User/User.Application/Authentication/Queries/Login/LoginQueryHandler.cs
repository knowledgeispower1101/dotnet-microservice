
using MediatR;
using Shared.Application.Common.Exceptions;
using Shared.Application.Common.Response;
using User.Application.Authentication.Persistence;

namespace User.Application.Authentication.Queries.Login;

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
        // var user = await _userRepository.GetUserByEmail(query.Email);
        // if (user is null || !_passwordHasher.Verify(query.Password, user.Password)) throw new WrongEmailPasswordException();
        // string refreshToken = _refreshTokens.Generate();
        // await _refreshTokenStore.SaveAsync(
        //     user.Id,
        //     refreshToken
        // );
        // var accessToken = _jwtTokenGenerator.GenerateToken(
        //     user.Id,
        //     user.FirstName,
        //     user.LastName,
        //     user.Email
        // );
        LoginResponse loginResponse = new(
                    1,
                   "query.Email",
                   "accessToken",
                   "refreshToken",
                   "user.LastName",
                   "user.FirstName"
            );
        return BaseResponse<LoginResponse>.Ok(loginResponse, "Login successfully");
    }

}