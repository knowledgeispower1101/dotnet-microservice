namespace ShoppeeClone.Application.Authentication.Queries.Profile;

public record UserResponse(int Id, string Email, string FirstName, string LastName);
public record ProfileResponse(string AccessToken, UserResponse User);