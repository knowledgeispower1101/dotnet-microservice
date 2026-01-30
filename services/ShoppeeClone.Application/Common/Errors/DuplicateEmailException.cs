namespace ShoppeeClone.Application.Common.Errors;

using System.Net;

public sealed class DuplicateEmailException : AppException
{
    public DuplicateEmailException()
        : base("Email already existed")
    {
    }

    public override string ErrorCode => "DUPLICATE_EMAIL";
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
