using Shared.Application.Common.Exceptions;
using Shared.Application.Common.Interface;
using Shared.Application.Common.Response;
using User.Interfaces;
using User.Models;

namespace User.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokens _refreshTokens;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IRefreshTokens refreshTokens,
        IRefreshTokenStore refreshTokenStore,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _refreshTokens = refreshTokens;
        _refreshTokenStore = refreshTokenStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<LoginResponse>> LoginAsync(string email, string password)
    {
        // Commented out original logic - keeping the mock response for now
        // var user = await _userRepository.GetUserByEmail(email);
        // if (user is null || !_passwordHasher.Verify(password, user.PasswordHash)) 
        //     throw new WrongEmailPasswordException();
        
        // string refreshToken = _refreshTokens.Generate();
        // await _refreshTokenStore.SaveAsync(user.Id, refreshToken);
        
        // var accessToken = _jwtTokenGenerator.GenerateToken(
        //     user.Id,
        //     user.FirstName,
        //     user.LastName,
        //     user.Email
        // );

        LoginResponse loginResponse = new(
            1,
            "query.Email",
            "accessToken",
            "refreshToken",
            "user.LastName",
            "user.FirstName"
        );
        
        return BaseResponse<LoginResponse>.Ok(loginResponse, "Login successfully");
    }

    public async Task<BaseResponse<string>> RegisterAsync(
        string username, 
        string email, 
        string password, 
        string firstName, 
        string lastName, 
        string phoneNumber)
    {
        await _unitOfWork.BeginTransactionAsync(CancellationToken.None);
        try
        {
            if (await _userRepository.GetUserByEmail(email) is not null)
                throw new DuplicateEmailException(email);

            string hashedPassword = _passwordHasher.Hash(password);

            _userRepository.Add(new UsersApp
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = hashedPassword,
                PhoneNumber = phoneNumber,
                Username = username
            });

            await _unitOfWork.CommitAsync(CancellationToken.None);

            return BaseResponse<string>.Ok(
                "Your account has been successfully created",
                "Register success");
        }
        catch
        {
            await _unitOfWork.RollbackAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<BaseResponse<string>> LogoutAsync(Guid userId)
    {
        // TODO: Get refresh token hash from userId or pass it as parameter
        // For now, this is a placeholder implementation
        // await _refreshTokenStore.RemoveAsync(refreshTokenHash);
        return BaseResponse<string>.Ok("Logout successfully", "Logout success");
    }
}
