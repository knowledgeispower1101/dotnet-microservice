namespace ShoppeeClone.Infrastructure;

using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Domain.Abstractions;
using ShoppeeClone.Domain.Entities;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}