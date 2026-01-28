using System.Net;
using ShoppeeClone.Application.Exceptions;

namespace ShoppeeClone.Application.Common.Errors;



public sealed class DuplicateEmailException : AppException
{
    public DuplicateEmailException()
        : base("Email already existed")
    {
    }

    public override string ErrorCode => "DUPLICATE_EMAIL";
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
