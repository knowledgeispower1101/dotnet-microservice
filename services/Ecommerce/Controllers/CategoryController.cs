using Ecommerce.Entities;
using Ecommerce.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("category")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetMenuCategories()
    {
        return Ok(await _categoryService.GetRootCategories());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null)
            return NotFound(new { message = "Category not found" });

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Create([FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _categoryService.CreateAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> Update(int id, [FromBody] Category category)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _categoryService.UpdateAsync(id, category);
        if (updated == null)
            return NotFound(new { message = "Category not found" });

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _categoryService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Category not found" });

        return NoContent();
    }

    [HttpGet("{id}/children")]
    public async Task<IActionResult> GetChildrenCategory(int id)
    {
        var result = await _categoryService.GetCategoriesByParentId(id);
        return Ok(result);
    }
}
