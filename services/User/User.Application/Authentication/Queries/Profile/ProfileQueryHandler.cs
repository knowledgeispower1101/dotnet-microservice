using MediatR;
using User.Application.Authentication.Persistence;
using Shared.Application.Common.Exceptions;
using Shared.Application.Common.Response;

namespace User.Application.Authentication.Queries.Profile;

public sealed class ProfileQueryHandler(
    IUserRepository userRepository,
    IRefreshTokenStore refreshTokenStore,
    IJwtTokenGenerator jwtTokenGenerator
) : IRequestHandler<ProfileQuery, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(
        ProfileQuery request,
        CancellationToken cancellationToken)
    {
        //  if (string.IsNullOrWhiteSpace(request.RefreshToken)) throw new BadRequestException("Refresh token is required");
        // var userId = await refreshTokenStore.GetUserIdAsync(request.RefreshToken) ?? throw new UnauthorizedException("Invalid or expired refresh token");
        // var user = await userRepository.GetUserById(userId) ?? throw new UnauthorizedException("User not found");
        // var accessToken = jwtTokenGenerator.GenerateToken(
        //     user.Id,
        //     user.FirstName,
        //     user.LastName,
        //     user.Email
        // );

        // var userResponse = new UserResponse(
        //     user.Id,
        //     user.Email,
        //     user.FirstName,
        //     user.LastName
        // );
        return BaseResponse<string>.Ok("null", "Profile retrieved successfully");
    }
}
