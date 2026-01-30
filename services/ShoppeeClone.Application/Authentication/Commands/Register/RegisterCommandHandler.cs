namespace ShoppeeClone.Application.Authentication.Commands.Register;

using MediatR;
using ShoppeeClone.Application.Common.Errors;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Application.Common.Response;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Domain.Entities;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<RegisterCommands, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<BaseResponse<string>> Handle(
        RegisterCommands commands,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmail(commands.Email) is not null) throw new DuplicateEmailException();

        string hashedPassword = _passwordHasher.Hash(commands.Password);

        await _userRepository.Add(new User
        {
            FirstName = commands.FirstName,
            LastName = commands.LastName,
            Email = commands.Email,
            Password = hashedPassword
        });

        return BaseResponse<string>.Ok(
            "Your account has been successfully created",
            "Register success");
    }
}
