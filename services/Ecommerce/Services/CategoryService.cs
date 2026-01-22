using System.Text.Json;
using Ecommerce.Data;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ecommerce.Services;

public class CategoryService(AppDbContext context, IConnectionMultiplexer redis) : ICategoryService
{
    private const string CacheKey = "category:menu";

    private readonly AppDbContext _context = context;
    private readonly IDatabase _cache = redis.GetDatabase();

    public async Task<IEnumerable<Category>> GetRootCategories()
    {
        var cached = await _cache.StringGetAsync(CacheKey);
        if (!cached.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<List<Category>>(cached!)!;
        }
        var categories = await _context.Categories
            .Where(c => c.ParentCategory == null)
            .ToListAsync();


        await _cache.StringSetAsync(
                        CacheKey,
                        JsonSerializer.Serialize(categories),
                        TimeSpan.FromMinutes(60 * 24)
        );
        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.ChildCategories)
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetByParentIdAsync(Guid? parentId)
    {
        return await _context.Categories
            .Where(c => c.ParentId == parentId)
            .Include(c => c.ChildCategories)
            .ToListAsync();
    }

    public async Task<Category> CreateAsync(Category category)
    {
        category.Id = Guid.NewGuid();
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(Guid id, Category category)
    {
        var existing = await _context.Categories.FindAsync(id);
        if (existing == null)
            return null;

        existing.Name = category.Name;
        existing.Description = category.Description;
        existing.ParentId = category.ParentId;
        existing.IconUrl = category.IconUrl;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
