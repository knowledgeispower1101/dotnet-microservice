namespace ShoppeeClone.Application.Authentication.Commands.Register;

using MediatR;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<BaseResponse<string>>, ITransactionalRequest;