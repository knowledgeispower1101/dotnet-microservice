using Ecommerce.Entities;

namespace Ecommerce.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetRootCategories();
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetByParentIdAsync(Guid? parentId);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(Guid id, Category category);
    Task<bool> DeleteAsync(Guid id);
}
