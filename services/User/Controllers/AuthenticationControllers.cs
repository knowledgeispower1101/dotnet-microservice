
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
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(
            request.UserName,
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.PhoneNumber);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(
            request.Email,
            request.Password
        );
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

    [HttpGet("profile")]
    public async Task<ActionResult> CurrentUser()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) 
            throw new BadRequestException();
        
        // TODO: Implement profile retrieval using refreshToken
        // This would require IUserService.GetProfileAsync implementation
        return Ok("Profile endpoint - implementation pending");
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) 
            throw new BadRequestException();
        
        // TODO: Extract userId from refreshToken
        // For now, using a placeholder
        var userId = Guid.Empty; // This should be extracted from the refresh token
        
        var response = await _authService.LogoutAsync(userId);
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