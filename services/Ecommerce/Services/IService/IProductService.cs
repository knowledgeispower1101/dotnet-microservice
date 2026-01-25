using Ecommerce.Entities;

namespace Ecommerce.Services.IService;

public interface IProductService
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<string>> GetCategoryTreeByProductId(int productId);
}
