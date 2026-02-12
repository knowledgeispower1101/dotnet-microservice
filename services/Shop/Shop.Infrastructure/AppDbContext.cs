using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // DbSet properties will be added here when domain entities are defined

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

}
