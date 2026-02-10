using MediatR;
using ShoppeeClone.Application.Authentication.Persistence;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Authentication.Commands.Logout;

public class LogoutCommandHandler(
        IRefreshTokenStore refreshTokenStore
) : IRequestHandler<LogoutCommand, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        string refreshToken = command.RefreshToken;
        await refreshTokenStore.RemoveAsync(refreshToken);
        return BaseResponse<string>.Ok("Logout successfully");
    }
}
