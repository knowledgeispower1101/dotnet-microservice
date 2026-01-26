namespace ShoppeeClone.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using ShoppeeClone.Application.Services.Authentication;
using ShoppeeClone.Constracts.Authentication;

[ApiController]
[Route("api/auth")]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        var response = new AuthenticationResponse(
            "asd",
            authResult.Email,
            [],
            authResult.AccessToken,
            "authResult.refreshToken"
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password
        );
        return Ok(new
        {
            userId = authResult.UserId,
        });
    }
}