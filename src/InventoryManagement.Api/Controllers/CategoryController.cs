using InventoryManagement.Api.Contracts.Categories;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    public CategoryController(CategoryService service)
    {
        _service = service;
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryRequest request)
    {
        var category = await _service.CreateCategoryAsync(request.Name);
        return CreatedAtAction(nameof(CategoryByID),new{id=category.Id},category);
    }
    [HttpGet]
    public async Task<IActionResult> AllCategories()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> CategoryByID(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if(category is null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _service.DeleteCategoryAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}