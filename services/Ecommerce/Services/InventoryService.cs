using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Services.Exception;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecommerce.Services;

public class InventoryService(AppDbContext context) : IInventoryService
{
    private readonly AppDbContext _context = context;

    public async Task<Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _context.Inventories
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<Inventory?> UpdateQuantityAsync(Guid productId, int quantity)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null)
        {
            // Create new inventory if doesn't exist
            inventory = new Inventory
            {
                ProductId = productId,
                Quantity = quantity,
                ReservedQuantity = 0,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Inventories.Add(inventory);
        }
        else
        {
            inventory.Quantity = quantity;
            inventory.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return inventory;
    }

    public async Task<bool> ReserveStockAsync(Guid productId, int quantity)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        if (inventory == null || inventory.AvailableQuantity < quantity)
            return false;

        inventory.ReservedQuantity += quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReleaseStockAsync(Guid productId, int quantity)
    {

        const string sql = """
                            UPDATE inventories
                            SET
                                reserved_quantity = reserved_quantity - @quantity,
                                updated_at = NOW()
                            WHERE
                                product_id = @productId
                                AND reserved_quantity >= @quantity;
                            """;

        var affected = await _context.Database.ExecuteSqlRawAsync(
            sql,
            new NpgsqlParameter("productId", productId),
            new NpgsqlParameter("quantity", quantity)
        );
        if (affected == 0)
            throw new OutOfStockException(
                "Cannot release more than reserved quantity"
            );
        return affected == 1;
    }

    public async Task<bool> CheckAvailabilityAsync(Guid productId, int quantity)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        return inventory != null && inventory.AvailableQuantity >= quantity;
    }
}
