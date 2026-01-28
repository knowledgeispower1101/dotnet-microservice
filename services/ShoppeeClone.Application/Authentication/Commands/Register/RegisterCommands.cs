namespace ShoppeeClone.Application.Authentication.Commands.Register;

using MediatR;

public record RegisterCommands(String FirstName, string LastName, string Email, string Password) : IRequest<RegisterResponse>;
