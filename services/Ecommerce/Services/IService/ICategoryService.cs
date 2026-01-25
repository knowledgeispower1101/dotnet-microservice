using Ecommerce.Entities;

namespace Ecommerce.Services.IService;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetRootCategories();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<CategoryRecord>> GetCategoriesByParentId(int id);
}

public record CategoryRecord(int Id, string Name);
