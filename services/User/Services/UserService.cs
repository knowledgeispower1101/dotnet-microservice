using Shared.Application.Common.Response;
using User.Interfaces;
using User.Models;

namespace User.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<BaseResponse<UserProfile>> CreateProfileAsync(Guid userId, UserProfile profile)
    {
        throw new NotImplementedException("Profile creation not yet implemented");
    }

    public async Task<BaseResponse<UserProfile>> GetProfileAsync(Guid userId)
    {
        throw new NotImplementedException("Profile retrieval not yet implemented");
    }
}
