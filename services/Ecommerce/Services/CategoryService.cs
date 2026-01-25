using System.Text.Json;
using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Services.IService;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        /*
           SELECT *
           FROM categories c
           WHERE c.parent_category = NULL;
        */
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

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }



    public async Task<Category> CreateAsync(Category category)
    {

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(int id, Category category)
    {
        var existing = await _context.Categories.FindAsync(id);
        if (existing == null)
            return null;

        existing.Name = category.Name;
        existing.Description = category.Description;
        existing.ParentId = category.ParentId;
        existing.IconUrl = category.IconUrl;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<CategoryRecord>> GetCategoriesByParentId(int id)
    {
        const string sql = """
        WITH RECURSIVE category_tree AS (
            SELECT id, name
            FROM categories
            WHERE id = @id

            UNION ALL

            SELECT c.id, c.name
            FROM categories c
            INNER JOIN category_tree ct ON c.parent_id = ct.id
        )
        SELECT * FROM category_tree;
    """;

        return await _context.Database
            .SqlQueryRaw<CategoryRecord>(sql, new NpgsqlParameter("id", id))
            .ToListAsync();
    }
}
