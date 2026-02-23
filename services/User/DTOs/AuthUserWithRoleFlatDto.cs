namespace User.DTOs;

public sealed class AuthUserWithRoleFlatDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = default!;
    public string PasswordHash { get; init; } = default!;
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string Username { get; init; } = default!;
    public required string[] RoleName { get; init; }
}