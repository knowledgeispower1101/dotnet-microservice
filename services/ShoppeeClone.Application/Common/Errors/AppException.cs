using System.Net;

namespace ShoppeeClone.Application.Common.Errors;

public abstract class AppException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
    public abstract HttpStatusCode StatusCode { get; }
}
