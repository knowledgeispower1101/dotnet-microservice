using Ecommerce.Entities;

namespace Ecommerce.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<IEnumerable<Product>> SearchAsync(string query);
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(Guid id, Product product);
    Task<bool> DeleteAsync(Guid id);
    Task<int> GetTotalCountAsync();
}
