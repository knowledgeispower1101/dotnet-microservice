using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

}
