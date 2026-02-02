namespace ShoppeeClone.Application.Authentication.Queries.Login;

using MediatR;
using ShoppeeClone.Application.Common.Response;

public record LoginQuery(string Email, string Password) : IRequest<BaseResponse<LoginResponse>>;
