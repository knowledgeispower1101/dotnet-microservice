namespace User.Application.Authentication.Commands.Register;

using MediatR;
using Shared.Application.Common.Response;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    string Username
) : IRequest<BaseResponse<string>>;