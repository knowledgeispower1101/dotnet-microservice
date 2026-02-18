namespace User.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, string firstName, string lastName, string email);
}