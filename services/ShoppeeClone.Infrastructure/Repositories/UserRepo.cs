using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShoppeeClone.Infrastructure.Repositories;

public class UserRepo(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext context = appDbContext;
    public async Task<User> Add(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        Console.WriteLine($"user is {user}");
        return user;
    }

}