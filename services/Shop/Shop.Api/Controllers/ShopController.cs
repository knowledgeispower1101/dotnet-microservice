using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Response;
using Shop.Application.Shop.Commands.CreateShop;
using Shop.Contracts.Shop;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/shop")]
public class ShopController(ISender mediator) : ControllerBase
{
    private readonly ISender _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateShop(CreateShopRequest request)
    {
        var command = new CreateShopCommand(
            request.Name,
            request.Description);
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetShops()
    {
        // Placeholder for getting shops
        await Task.CompletedTask;
        return Ok(BaseResponse<string>.Ok("List of shops", "Success"));
    }
}
