namespace User.Application.Authentication.Queries.Login;

using MediatR;
using Shared.Application.Common.Response;

public record LoginQuery(string Email, string Password) : IRequest<BaseResponse<LoginResponse>>;
