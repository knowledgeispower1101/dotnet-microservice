
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.Application.Authentication.Commands.Register;
using User.Application.Authentication.Queries.Login;
using User.Application.Authentication.Queries.Profile;
using User.Application.Common.Errors;
using User.Constracts.Authentication;

namespace User.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationControllers(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );
        var response = await _mediator.Send(query);
        Response.Cookies.Append(
            "refreshToken",
            response.Data!.ResetToken,
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
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) throw new BadRequestException();
        Console.WriteLine(refreshToken);
        var query = new ProfileQuery(refreshToken);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)) throw new BadRequestException();
        var query = new ProfileQuery(refreshToken);
        var response = await _mediator.Send(query);
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