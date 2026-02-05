using MediatR;
using ShoppeeClone.Application.Common.Response;

namespace ShoppeeClone.Application.Authentication.Queries.Profile;

public record ProfileQuery(string RefreshToken) : IRequest<BaseResponse<ProfileResponse>>;