using Microsoft.EntityFrameworkCore;
using User.Interfaces;
using User.Models;

namespace User.Data;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<UsersApp?> GetUserByEmail(string email) =>
        await _dbContext.UsersApp
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

    public void Add(UsersApp user) =>
        _dbContext.UsersApp.Add(user);

    public async Task<UsersApp?> GetUserById(Guid id) =>
        await _dbContext.UsersApp
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
}
