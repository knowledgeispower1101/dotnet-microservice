
using MediatR;
using ShoppeeClone.Application.Authentication.Persistence;
using ShoppeeClone.Application.Common.Errors;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;
using ShoppeeClone.Domain.Entities;

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
        int id = await _refreshTokenStore.GetUserIdAsync(request.RefreshToken) ?? throw new BadRequestException("No id was found");
        User user = await _userRepository.GetUserById(id) ?? throw new BadRequestException("User is not found");
        var userResponse = new UserResponse(
            id,
            user.Email,
            user.FirstName,
            user.LastName
        );
        var response = new ProfileResponse("", userResponse);
        return null;
    }
}