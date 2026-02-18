using Shared.Application.Common.Response;
using User.Models;

namespace User.Interfaces;

public interface IUserService
{
    Task<BaseResponse<UserProfile>> CreateProfileAsync(Guid userId, UserProfile profile);
    Task<BaseResponse<UserProfile>> GetProfileAsync(Guid userId);
}
