using MediatR;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Authentication.Commands.Logout;

public class LogoutCommandHandler() : IRequestHandler<LogoutCommands, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(LogoutCommands request, CancellationToken cancellationToken)
    {
        return BaseResponse<string>.Ok("Logout successfully");
    }

}
