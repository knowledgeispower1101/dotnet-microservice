namespace ShoppeeClone.Application.Authentication.Commands.Register;

using MediatR;
using ShoppeeClone.Application.Common.Errors;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

public class RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommands, RegisterResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public async Task<RegisterResponse> Handle(RegisterCommands commands, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmail(commands.Email) is User userDB) throw new DuplicateEmailException();
        string hashedPassword = _passwordHasher.Hash(commands.Password);
        User user = await _userRepository.Add(new User
        {
            FirstName = commands.FirstName,
            LastName = commands.LastName,
            Email = commands.Email,
            Password = hashedPassword
        });

        return new RegisterResponse(user.Id, user.Email, user.LastName, user.FirstName);
    }
}