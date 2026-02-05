namespace ShoppeeClone.Application.Authentication.Persistence;

using ShoppeeClone.Domain.Entities;
public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User> Add(User user);
    Task<User?> GetUserById(int id);
}