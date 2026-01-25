namespace Ecommerce.Entities;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public int ReservedQuantity { get; set; } = 0;
    public int AvailableQuantity => Quantity - ReservedQuantity;
    public Product? Product { get; set; }
}
