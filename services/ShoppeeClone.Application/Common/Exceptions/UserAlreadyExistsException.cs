namespace ShoppeeClone.Application.Common.Exceptions;

public sealed class UserAlreadyExistsException(string email) : AppException($"User with email '{email}' already exists.")
{
    public string Email { get; } = email;

    public override string ErrorCode => "USER_ALREADY_EXISTS";
}
