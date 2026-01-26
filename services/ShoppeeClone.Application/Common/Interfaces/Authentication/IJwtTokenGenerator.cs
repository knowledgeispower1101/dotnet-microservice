namespace ShoppeeClone.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string firstName, string lastName, string email);
}