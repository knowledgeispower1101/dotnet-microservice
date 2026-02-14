using MediatR;
using Shared.Application.Common.Exceptions;
using Shared.Application.Common.Interface;
using Shared.Application.Common.Response;
using User.Application.Authentication.Persistence;
using User.Domain.Models;

namespace User.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IUnitOfWork _unitOfWork,
    IUserRepository _userRepo,
    IPasswordHasher _passwordHasher
) : IRequestHandler<RegisterCommand, BaseResponse<string>>
{
    private readonly IPasswordHasher passwordHasher = _passwordHasher;
    private readonly IUnitOfWork unitOfWork = _unitOfWork;
    private readonly IUserRepository userRepo = _userRepo;
    public async Task<BaseResponse<string>> Handle(
        RegisterCommand commands,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (await userRepo.GetUserByEmail(commands.Email) is not null) throw new DuplicateEmailException(commands.Email);
            string hashedPassword = passwordHasher.Hash(commands.Password);
            userRepo.Add(new UsersApp
            {
                FirstName = commands.FirstName,
                LastName = commands.LastName,
                Email = commands.Email,
                PasswordHash = hashedPassword,
                PhoneNumber = commands.PhoneNumber,
                Username = commands.Username
            });
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
        }


        return BaseResponse<string>.Ok(
            "Your account has been successfully created",
            "Register success");
    }
}
