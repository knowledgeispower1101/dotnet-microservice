namespace ShoppeeClone.Application.Exceptions;

public abstract class AppException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
}
