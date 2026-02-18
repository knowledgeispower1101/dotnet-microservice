using System;
using System.Collections.Generic;

namespace User.Models;

public partial class UserProfile
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Bio { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual UsersApp User { get; set; } = null!;
}
