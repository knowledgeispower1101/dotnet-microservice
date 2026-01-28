namespace ShoppeeClone.Application.Common.Errors;

using System.Net;
public sealed class DuplicateEmailException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => "Email already existed";
}
