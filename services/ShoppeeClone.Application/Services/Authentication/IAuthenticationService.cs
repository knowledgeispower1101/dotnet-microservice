using ShoppeeClone.Domain.Entities;

namespace ShoppeeClone.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> Login(string email, string password);
    Task<User> Register(string firstName, string lastName, string email, string password);
}