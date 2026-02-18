using Microsoft.EntityFrameworkCore;
using User.Interfaces;
using User.Models;

namespace User.Repositories;

public class UserRepo(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _context = appDbContext;

    public void Add(UsersApp user)
    {
        _context.UsersApps.Add(user);
    }

    public async Task<UsersApp?> GetUserByEmail(string email)
    {
        return await _context.UsersApps
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<UsersApp?> GetUserById(Guid id)
    {
        return await _context.UsersApps
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
