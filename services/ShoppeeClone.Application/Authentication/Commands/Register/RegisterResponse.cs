namespace ShoppeeClone.Application.Authentication.Commands.Register;

public record RegisterResponse(int UserId, string Email, string LastName, string FirstName);