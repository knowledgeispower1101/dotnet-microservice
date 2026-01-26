namespace ShoppeeClone.Application.Services.Persistence;

using ShoppeeClone.Domain.Entities;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}