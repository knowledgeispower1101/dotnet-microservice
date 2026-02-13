namespace User.Domain.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public Role Role { get; set; } = null!;

    public UsersApp User { get; set; } = null!;
}
