using Ecommerce.Entities;
using Ecommerce.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("inventory")]
public class InventoryController(IInventoryService inventoryService) : ControllerBase
{
    private readonly IInventoryService _inventoryService = inventoryService;

    [HttpGet("{productId}")]
    public async Task<ActionResult<Inventory>> GetByProductId(int productId)
    {
        var inventory = await _inventoryService.GetByProductIdAsync(productId);
        if (inventory == null)
            return NotFound(new { message = "Inventory not found for this product" });

        return Ok(inventory);
    }

    [HttpPut("{productId}/quantity")]
    public async Task<ActionResult<Inventory>> UpdateQuantity(int productId, [FromBody] UpdateQuantityRequest request)
    {
        if (request.Quantity < 0)
            return BadRequest(new { message = "Quantity cannot be negative" });

        var inventory = await _inventoryService.UpdateQuantityAsync(productId, request.Quantity);
        if (inventory == null)
            return NotFound(new { message = "Product not found" });

        return Ok(inventory);
    }

    [HttpPost("{productId}/reserve")]
    public async Task<ActionResult> ReserveStock(int productId, [FromBody] ReserveStockRequest request)
    {
        if (request.Quantity <= 0)
            return BadRequest(new { message = "Quantity must be greater than zero" });

        var success = await _inventoryService.ReserveStockAsync(productId, request.Quantity);
        if (!success)
            return BadRequest(new { message = "Insufficient stock available" });

        return Ok(new { message = "Stock reserved successfully" });
    }

    [HttpPost("{productId}/release")]
    public async Task<ActionResult> ReleaseStock(int productId, [FromBody] ReleaseStockRequest request)
    {
        if (request.Quantity <= 0)
            return BadRequest(new { message = "Quantity must be greater than zero" });

        var success = await _inventoryService.ReleaseStockAsync(productId, request.Quantity);
        if (!success)
            return BadRequest(new { message = "Cannot release more than reserved quantity" });

        return Ok(new { message = "Stock released successfully" });
    }

    [HttpGet("{productId}/check")]
    public async Task<ActionResult<object>> CheckAvailability(int productId, [FromQuery] int quantity)
    {
        if (quantity <= 0)
            return BadRequest(new { message = "Quantity must be greater than zero" });

        var available = await _inventoryService.CheckAvailabilityAsync(productId, quantity);
        return Ok(new { productId, requestedQuantity = quantity, available });
    }
}

public record UpdateQuantityRequest(int Quantity);
public record ReserveStockRequest(int Quantity);
public record ReleaseStockRequest(int Quantity);
