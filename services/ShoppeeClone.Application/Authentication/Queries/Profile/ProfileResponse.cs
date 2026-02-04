namespace ShoppeeClone.Application.Authentication.Queries.Profile;

public record User(int Id, string Email, string FirstName, string LastName);
public record ProfileResponse(string AccessToken, User User);