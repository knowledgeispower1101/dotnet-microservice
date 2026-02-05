namespace ShoppeeClone.Application.Authentication.Commands.Logout;

using MediatR;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;

public record LogoutCommands(
    string RefreshToken
) : IRequest<BaseResponse<string>>, ITransactionalRequest;