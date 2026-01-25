using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class ProductService(AppDbContext context) : IProductService
{
    private readonly AppDbContext _context = context;

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {

        _context.Products.Add(product);

        var inventory = new Inventory
        {
            ProductId = product.Id,
            Quantity = 0,
            ReservedQuantity = 0,
        };
        _context.Inventories.Add(inventory);

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product product)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null)
            return null;

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.CategoryId = product.CategoryId;
        existing.ImageUrl = product.ImageUrl;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
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

    public Task<IEnumerable<string>> GetCategoryTreeByProductId(int productId)
    {
        throw new NotImplementedException();
    }
}
