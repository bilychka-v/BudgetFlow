using Application.Exceptions;
using Application.Interfaces;
using BudgetFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var category = await _categoryService.AddCategoryAsync(categoryDto);
        
        return CreatedAtAction(nameof(GetCategories), new { userId = category.UserId }, category);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto categoryDto)
    {
        var category = await _categoryService.UpdateCategoryAsync(id, categoryDto);

        if (category is null)
           throw new NotFoundException("Category not found");

        return Ok(category);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}