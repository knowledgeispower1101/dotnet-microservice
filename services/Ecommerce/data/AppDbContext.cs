using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    private static readonly string CATEGORY = "category";
    private static readonly string PRODUCT = "product";
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(builder =>
        {
            builder.ToTable(CATEGORY);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.ToTable(PRODUCT);
        });


        // modelBuilder.Entity<Category>(entity =>
        // {
        //     entity.HasKey(c => c.Id);
        //     entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        //     entity.Property(c => c.Description).HasMaxLength(500);
        //     entity.Property(c => c.IconUrl).HasMaxLength(500);

        //     entity.HasOne(c => c.ParentCategory)
        //         .WithMany(c => c.ChildCategories)
        //         .HasForeignKey(c => c.ParentId)
        //         .OnDelete(DeleteBehavior.Restrict);

        //     entity.HasIndex(c => c.Name);
        // });

        // modelBuilder.Entity<Product>(entity =>
        // {
        //     entity.HasKey(p => p.Id);
        //     entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
        //     entity.Property(p => p.Description).HasMaxLength(1000);
        //     entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
        //     entity.Property(p => p.ImageUrl).HasMaxLength(500);


        //     entity.HasOne(p => p.Category)
        //         .WithMany(c => c.Products)
        //         .HasForeignKey(p => p.CategoryId)
        //         .OnDelete(DeleteBehavior.Restrict);

        //     entity.HasIndex(p => p.Name);
        //     entity.HasIndex(p => p.CategoryId);
        //     entity.HasIndex(p => p.Price);
        // });

        // modelBuilder.Entity<Inventory>(entity =>
        // {
        //     entity.HasKey(i => i.Id);
        //     entity.Property(i => i.Quantity).IsRequired().HasDefaultValue(0);
        //     entity.Property(i => i.ReservedQuantity).IsRequired().HasDefaultValue(0);


        //     entity.HasOne(i => i.Product)
        //         .WithOne(p => p.Inventory)
        //         .HasForeignKey<Inventory>(i => i.ProductId)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     entity.HasIndex(i => i.ProductId).IsUnique();
        // });

        // modelBuilder.Entity<Cart>(builder =>
        // {
        //     builder.ToTable("cart");
        //     builder.HasKey(c => c.Id);
        //     builder.HasIndex(c => c.UserId).IsUnique();


        // });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private static void UpdateTimestamps()
    {
        // var entries = ChangeTracker.Entries()
        //     .Where(e => e.State == EntityState.Modified);

        // foreach (var entry in entries)
        // {
        //     if (entry.Entity is Category category)
        //         category.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is Product product)
        //         product.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is Inventory inventory)
        //         inventory.UpdatedAt = DateTime.UtcNow;
        // }
    }
}
