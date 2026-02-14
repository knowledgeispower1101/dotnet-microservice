using User.Domain.Models;
namespace User.Application.Common.Persistence;

public interface IAppDbContext
{
    ISet<UsersApp> UsersApps { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
