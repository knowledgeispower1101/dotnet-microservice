using User.Models;

namespace User.Interfaces;

public interface IUserRepository
{
    Task<UsersApp?> GetUserByEmail(string email);
    void Add(UsersApp user);
    Task<UsersApp?> GetUserById(Guid id);
    // Task<UserProfile> AddProfile();
}