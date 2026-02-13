namespace User.Infrastructure.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual UsersApp User { get; set; } = null!;
}
