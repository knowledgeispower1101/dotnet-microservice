namespace ShoppeeClone.Constracts.Authentication;

public record AuthenticationResponse(
    Guid UserId,
    string Email,
    string[] Roles,
    string AccessToken,
    string RefreshToken
);