using Ecommerce.Entities;

namespace Ecommerce.Services;

public interface IInventoryService
{
    Task<Inventory?> GetByProductIdAsync(Guid productId);
    Task<Inventory?> UpdateQuantityAsync(Guid productId, int quantity);
    Task<bool> ReserveStockAsync(Guid productId, int quantity);
    Task<bool> ReleaseStockAsync(Guid productId, int quantity);
    Task<bool> CheckAvailabilityAsync(Guid productId, int quantity);
}
