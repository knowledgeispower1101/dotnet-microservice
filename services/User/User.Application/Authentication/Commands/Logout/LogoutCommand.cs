namespace User.Application.Authentication.Commands.Logout;

using MediatR;
using User.Application.Common.Interfaces;
using User.Application.Common.Response;

public record LogoutCommand
(
    string RefreshToken
) : IRequest<BaseResponse<string>>, ITransactionalRequest;