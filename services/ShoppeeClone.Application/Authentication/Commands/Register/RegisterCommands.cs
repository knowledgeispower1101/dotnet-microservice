namespace ShoppeeClone.Application.Authentication.Commands.Register;

using MediatR;
using ShoppeeClone.Application.Common.Interfaces;

public record RegisterCommands(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<RegisterResponse>, ITransactionalRequest;