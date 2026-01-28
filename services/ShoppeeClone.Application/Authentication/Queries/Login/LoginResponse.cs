namespace ShoppeeClone.Application.Authentication.Queries.Login;

public record LoginResponse(int UserId, string Email, string AccessToken, string LastName, string FirstName);