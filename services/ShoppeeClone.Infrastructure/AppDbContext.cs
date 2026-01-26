namespace ShoppeeClone.Infrastructure;

using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Domain.Abstractions;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}