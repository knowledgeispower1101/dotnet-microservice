namespace User.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstName, string lastName, string email, string username, string[] rolename);
    bool ValidateToken(string token);
}