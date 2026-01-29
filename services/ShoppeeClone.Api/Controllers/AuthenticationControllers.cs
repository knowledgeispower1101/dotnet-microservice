namespace ShoppeeClone.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppeeClone.Application.Authentication.Commands.Register;
using ShoppeeClone.Application.Authentication.Queries.Login;
using ShoppeeClone.Constracts.Authentication;


[ApiController]
[Route("api/auth")]
public class AuthenticationControllers(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommands(request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        var authResult = await _mediator.Send(command);
        return Ok(authResult);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );
        LoginResponse response = await _mediator.Send(query);
        Response.Cookies.Append(
            "refreshToken",
            response.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            }
        );
        return Ok(response);
    }
}