namespace User.Application.Authentication.Commands.Register;

using MediatR;
using User.Application.Common.Response;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<BaseResponse<string>>;