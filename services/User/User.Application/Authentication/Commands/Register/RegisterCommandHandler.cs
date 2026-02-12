
using MediatR;
using Shared.Application.Common.Response;
using User.Application.Authentication.Persistence;

namespace User.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher
) : IRequestHandler<RegisterCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<BaseResponse<string>> Handle(
        RegisterCommand commands,
        CancellationToken cancellationToken)
    {
        // if (await _userRepository.GetUserByEmail(commands.Email) is not null) throw new DuplicateEmailException();

        // string hashedPassword = _passwordHasher.Hash(commands.Password);

        // await _userRepository.Add(new User
        // {
        //     FirstName = commands.FirstName,
        //     LastName = commands.LastName,
        //     Email = commands.Email,
        //     Password = hashedPassword
        // });

        return BaseResponse<string>.Ok(
            "Your account has been successfully created",
            "Register success");
    }
}
