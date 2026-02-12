using MediatR;
using User.Application.Common.Response;

namespace User.Application.Authentication.Queries.Profile;

public record ProfileQuery(string RefreshToken) : IRequest<BaseResponse<string>>;