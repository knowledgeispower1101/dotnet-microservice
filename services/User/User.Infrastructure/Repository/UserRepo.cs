using Microsoft.EntityFrameworkCore;
using User.Application.Authentication.Persistence;
using User.Domain.Models;

namespace User.Infrastructure.Repository;

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

    public async Task<UsersApp?> GetUserById(int id)
    {
        return await _context.UsersApps
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
