namespace User.Infrastructure.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string PermissionName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = [];
}
