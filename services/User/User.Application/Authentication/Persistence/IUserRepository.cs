using User.Domain.Models;

namespace User.Application.Authentication.Persistence;

public interface IUserRepository
{
    Task<UsersApp?> GetUserByEmail(string email);
    void Add(UsersApp user);
    Task<UsersApp?> GetUserById(int id);
}