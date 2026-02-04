
using MediatR;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;
using ShoppeeClone.Application.Services.Persistence;

namespace ShoppeeClone.Application.Authentication.Queries.Profile;

public class ProfileQueryHandler(
        IUserRepository userRepository,
        IRefreshTokenStore refreshTokenStore
) : IRequestHandler<ProfileQuery, BaseResponse<ProfileResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRefreshTokenStore _refreshTokenStore = refreshTokenStore;

    public async Task<BaseResponse<ProfileResponse>> Handle(ProfileQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.RefreshToken + "RefreshToken");
        Console.WriteLine(await _refreshTokenStore.GetUserIdAsync(request.RefreshToken));
        return null;
    }
}