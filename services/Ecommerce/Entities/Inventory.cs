using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities;

[Table("inventories")]
public class Inventory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public int Quantity { get; set; } = 0;

    [Required]
    public int ReservedQuantity { get; set; } = 0;

    [NotMapped]
    public int AvailableQuantity => Quantity - ReservedQuantity;

    [Timestamp]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
}
