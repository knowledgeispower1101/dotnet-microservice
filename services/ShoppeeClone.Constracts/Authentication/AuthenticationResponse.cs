namespace ShoppeeClone.Constracts.Authentication;

public record AuthenticationResponse(
    string UserId,
    string Email,
    string[] Roles,
    string AccessToken,
    string RefreshToken
);