using Microsoft.AspNetCore.Mvc;
using User.Interfaces;

namespace User.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileControllers(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpGet]
    public Task<ActionResult> CurrentUser()
    {
        throw new NotImplementedException();

        // if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        //     throw new BadRequestException();

        // TODO: Implement profile retrieval using refreshToken
        // This would require IUserService.GetProfileAsync implementation
        // return Ok("Profile endpoint - implementation pending");
    }
}