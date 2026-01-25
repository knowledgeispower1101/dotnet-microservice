using Ecommerce.Entities;

namespace Ecommerce.Services.IService;

public interface IInventoryService
{
    Task<Inventory?> GetByProductIdAsync(int productId);
    Task<Inventory?> UpdateQuantityAsync(int productId, int quantity);
    Task<bool> ReserveStockAsync(int productId, int quantity);
    Task<bool> ReleaseStockAsync(int productId, int quantity);
    Task<bool> CheckAvailabilityAsync(int productId, int quantity);
}
