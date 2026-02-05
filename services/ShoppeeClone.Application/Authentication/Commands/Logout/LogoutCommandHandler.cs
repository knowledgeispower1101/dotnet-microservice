using MediatR;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Authentication.Commands.Logout;

public class LogoutCommandHandler(
        IRefreshTokenStore refreshTokenStore
) : IRequestHandler<LogoutCommands, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(LogoutCommands request, CancellationToken cancellationToken)
    {
        string refreshToken = request.RefreshToken;
        await refreshTokenStore.RemoveAsync(refreshToken);
        return BaseResponse<string>.Ok("Logout successfully");
    }
}
