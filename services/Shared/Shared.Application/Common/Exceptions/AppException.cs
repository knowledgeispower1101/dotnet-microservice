using System.Net;

namespace Shared.Application.Common.Exceptions;

public abstract class AppException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
    public abstract HttpStatusCode StatusCode { get; }
}
