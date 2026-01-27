namespace ShoppeeClone.Application.Services.Authentication;

public record AuthenticationResult(int UserId, string Email, string AccessToken, string LastName, string FirstName);