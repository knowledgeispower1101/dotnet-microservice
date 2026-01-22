using Ecommerce.Data;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class ProductService(AppDbContext context) : IProductService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventories)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventories)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Include(p => p.Inventories)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string query)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(query) || (p.Description != null && p.Description.Contains(query)))
            .Include(p => p.Category)
            .Include(p => p.Inventories)
            .ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _context.Products.Add(product);

        // Create initial inventory entry
        var inventory = new Inventory
        {
            ProductId = product.Id,
            Quantity = 0,
            ReservedQuantity = 0,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Inventories.Add(inventory);

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Guid id, Product product)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null)
            return null;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.CategoryId = product.CategoryId;
        existing.ImageUrl = product.ImageUrl;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Products.CountAsync();
    }

    public Task<List<string>> GetCategoryTreeByProductId(Guid productId)
    {
        throw new NotImplementedException();
    }
}
