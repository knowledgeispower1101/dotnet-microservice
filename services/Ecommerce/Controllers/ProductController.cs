using Ecommerce.Entities;
using Ecommerce.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("product")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
            return NotFound(new { message = "Product not found" });

        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryId(int categoryId)
    {
        var products = await _productService.GetByCategoryIdAsync(categoryId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _productService.CreateAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _productService.UpdateAsync(id, product);
        if (updated == null)
            return NotFound(new { message = "Product not found" });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Product not found" });

        return NoContent();
    }
}
