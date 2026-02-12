using System.Net;

namespace Shared.Application.Common.Exceptions;

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException()
        : base("Unauthorized")
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public override string ErrorCode => "UNAUTHORIZED";
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
