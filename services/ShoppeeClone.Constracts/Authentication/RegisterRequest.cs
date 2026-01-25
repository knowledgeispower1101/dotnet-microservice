namespace ShoppeeClone.Constracts.Authentication;

public record RegisterRequest(String FirstName, string LastName, string Email, string Password);