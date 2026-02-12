using System.Net;

namespace Shared.Application.Common.Exceptions;

public sealed class DuplicateEmailException : AppException
{
    public DuplicateEmailException(string email)
        : base($"Email '{email}' is already registered")
    {
    }

    public override string ErrorCode => "DUPLICATE_EMAIL";
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
