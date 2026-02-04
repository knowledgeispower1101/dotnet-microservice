namespace ShoppeeClone.Application.Common.Errors;

using System.Net;

public sealed class BadRequestException : AppException
{
    public BadRequestException()
        : base("Bad request")
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public override string ErrorCode => "BAD_REQUEST";
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}

