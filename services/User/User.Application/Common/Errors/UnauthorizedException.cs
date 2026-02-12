namespace User.Application.Common.Errors;

using System.Net;

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException()
        : base("Unauthorized request")
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public override string ErrorCode => "UNAUTHORIZED_REQUEST";
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}

