namespace ShoppeeClone.Application.Authentication.Queries.Login;

using MediatR;

public record LoginQuery(string Email, string Password) : IRequest<LoginResponse>;
