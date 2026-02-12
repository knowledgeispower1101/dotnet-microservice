namespace User.Application.Authentication.Commands.Logout;

using MediatR;
using Shared.Application.Common.Response;

public record LogoutCommand
(
    string RefreshToken
) : IRequest<BaseResponse<string>>;