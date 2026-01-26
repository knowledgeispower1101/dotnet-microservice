using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Infrastructure.Repositories;

public class UserRepo : IUserRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}