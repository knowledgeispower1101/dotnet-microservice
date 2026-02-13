namespace User.Infrastructure.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } =  [];

    public virtual ICollection<Permission> Permissions { get; set; } = [];
}
