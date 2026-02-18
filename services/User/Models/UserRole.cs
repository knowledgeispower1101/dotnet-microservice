using System;
using System.Collections.Generic;

namespace User.Models;

public partial class UserRole
{
    private UserRole() { }
    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime AssignedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual UsersApp User { get; set; } = null!;
}
