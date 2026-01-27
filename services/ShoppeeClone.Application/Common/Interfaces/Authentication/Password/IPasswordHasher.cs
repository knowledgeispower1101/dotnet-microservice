namespace ShoppeeClone.Application.Common.Interfaces.Authentication.Password;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}
