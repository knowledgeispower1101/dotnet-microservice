using MediatR;
using ShoppeeClone.Application.Authentication.Persistence;
using ShoppeeClone.Application.Common.Errors;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Authentication.Queries.Profile;

public sealed class ProfileQueryHandler(
    IUserRepository userRepository,
    IRefreshTokenStore refreshTokenStore,
    IJwtTokenGenerator jwtTokenGenerator
) : IRequestHandler<ProfileQuery, BaseResponse<ProfileResponse>>
{
    public async Task<BaseResponse<ProfileResponse>> Handle(
        ProfileQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken)) throw new BadRequestException("Refresh token is required");
        var userId = await refreshTokenStore.GetUserIdAsync(request.RefreshToken) ?? throw new UnauthorizedException("Invalid or expired refresh token");
        var user = await userRepository.GetUserById(userId) ?? throw new UnauthorizedException("User not found");
        var accessToken = jwtTokenGenerator.GenerateToken(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email
        );

        var userResponse = new UserResponse(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName
        );
        return BaseResponse<ProfileResponse>.Ok(new ProfileResponse(accessToken, userResponse), "Profile retrieved successfully");
    }
}
