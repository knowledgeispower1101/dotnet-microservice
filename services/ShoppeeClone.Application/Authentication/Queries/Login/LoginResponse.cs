namespace ShoppeeClone.Application.Authentication.Queries.Login;

public record LoginResponse(int UserId, string Email, string AccessToken, string ResetToken, string LastName, string FirstName);