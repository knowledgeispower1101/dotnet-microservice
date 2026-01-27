namespace ShoppeeClone.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using ShoppeeClone.Application.Services.Authentication;
using ShoppeeClone.Constracts.Authentication;

[ApiController]
[Route("api/auth")]
public class AuthenticationControllers(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        return Ok(authResult);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var authResult = _authenticationService.Login(
            request.Email,
            request.Password
        );
        // var response = new AuthenticationResponse(authResult.Id, authResul)
        //     Response.Cookies.Append(
        //        "refreshToken",
        //        authResult.!,
        //        new CookieOptions
        //        {
        //            HttpOnly = true,
        //            Secure = true, // HTTPS only
        //            SameSite = SameSiteMode.Strict,
        //            Expires = DateTimeOffset.UtcNow.AddDays(7)
        //        }
        //    );
        return Ok(authResult);
    }
}