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
    [Route("{userId}")]
    public async Task<IActionResult> GetCategories([FromRoute] Guid userId)
    {
        var categories = await _categoryService.GetCategoriesAsync(userId);
        
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var category = await _categoryService.AddCategoryAsync(categoryDto.UserId, categoryDto);
        
        return CreatedAtAction(nameof(GetCategories), new { userId = category.UserId }, category);
    }

    [HttpPut]
    [Route("{userId}/{id}")]
    public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromRoute] Guid userId, [FromBody] UpdateCategoryDto categoryDto)
    {
        var category = await _categoryService.UpdateCategoryAsync(userId, id, categoryDto);

        if (category is null)
           throw new NotFoundException("Category not found");

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