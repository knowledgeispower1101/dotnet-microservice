namespace ShoppeeClone.Application.Services.Persistence;

using ShoppeeClone.Domain.Entities;
public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<User> Add(User user);
}