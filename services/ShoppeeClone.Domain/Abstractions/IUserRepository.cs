using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Domain.Abstractions;

public interface IUserRepository
{
    void Insert(User user);
}