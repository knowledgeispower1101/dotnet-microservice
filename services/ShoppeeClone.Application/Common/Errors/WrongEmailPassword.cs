namespace ShoppeeClone.Application.Common.Errors;

using System.Net;
public sealed class WrongEmailPasswordException : AppException
{
    public WrongEmailPasswordException()
        : base("Email or password is incorrect")
    {
    }
    public override string ErrorCode => "WRONG_EMAIL_OR_PASSWORD";
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
