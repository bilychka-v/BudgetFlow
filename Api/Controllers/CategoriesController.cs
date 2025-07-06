using Application.Interfaces;
using BudgetFlow.Dtos;
using Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ICategoryService _categoryService;
    public CategoriesController(DataContext context, ICategoryService categoryService)
    {
        _context = context;
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
    [Route("{id}")]
    public async Task<IActionResult> PutCategory(Guid id, [FromBody] UpdateCategoryDto categoryDto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id);

        if (category is null)
            return NotFound();

        category.Name = categoryDto.Name;
        category.Type = categoryDto.Type;

        await _context.SaveChangesAsync();

        var response = new CategoryDto(
            category.Id,
            category.UserId,
            category.Name,
            category.Type
        );

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(t => t.Id == id);
        
        if (category is null)
            return NotFound();
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}