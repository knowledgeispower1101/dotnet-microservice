namespace ShoppeeClone.Application.Authentication.Queries.Login;

public record LoginResponse(int UserId, string Email, string RefreshToken, string LastName, string FirstName);