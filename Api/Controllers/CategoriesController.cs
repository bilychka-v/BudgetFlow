using Application.Interfaces;
using BudgetFlow.Dtos;
using Db;
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
    [Route("{userId}")]
    public async Task<IActionResult> GetCategories([FromRoute] Guid userId)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var categories = await _categoryService.GetCategoriesAsync(userId);
        
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
    {
        var category = await _categoryService.AddCategoryAsync(categoryDto.UserId, categoryDto);
        
        return CreatedAtAction(nameof(GetCategories), new { categoryId = category.Id }, category);
    }

    [HttpPut]
    [Route("{userId}/{id}")]
    public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromRoute] Guid userId, [FromBody] UpdateCategoryDto categoryDto)
    {
        var category = await _categoryService.UpdateCategoryAsync(userId, id, categoryDto);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpDelete]
    [Route("{userId}/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid userId, [FromRoute] Guid id)
    {
        await _categoryService.DeleteCategoryAsync(userId, id);
        return NoContent();
    }
}