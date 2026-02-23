
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Common.Exceptions;
using User.DTOs;
using User.Interfaces;

namespace User.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationControllers(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.UserRegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        Response.Cookies.Append(
            "refreshToken",
            response.Data!.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            }
        );
        return Ok(response);
    }

    [HttpGet("verify")]
    public Task<bool> VerifyToken()
    {
        var request = Request;
        return _authService.VerifyToken(request);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            throw new BadRequestException();

        var response = await _authService.LogoutAsync(refreshToken);
        Response.Cookies.Append(
            "refreshToken",
            "",
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            }
        );
        return Ok(response);
    }
}