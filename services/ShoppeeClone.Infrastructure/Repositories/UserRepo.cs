using ShoppeeClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Application.Authentication.Persistence;

namespace ShoppeeClone.Infrastructure.Repositories;

public class UserRepo(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _context = appDbContext;

    public Task<User> Add(User user)
    {
        _context.Users.Add(user);
        return Task.FromResult(user);
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}