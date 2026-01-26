namespace ShoppeeClone.Application.Services.Authentication;

public record AuthenticationResult(string UserId, string Email, string FirstName, string LastName, string AccessToken);