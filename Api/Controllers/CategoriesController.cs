using BudgetFlow.Dtos;
using Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetFlow.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoriesController:ControllerBase
{
    private readonly DataContext _context;
    public CategoriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        
        var response = categories.Select(t=> new CategoryDto(
            Id: t.Id,
            UserId: t.UserId,
            Name: t.Name,
            Type: t.Type)).ToList();
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostCategory([FromBody] CategoryDto categoryDto)
    {
        var category = new Category
        {
            Id = new Guid(),
            UserId = categoryDto.UserId,
            Name = categoryDto.Name,
            Type = categoryDto.Type
        };
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        var response = new CategoryDto(
            category.Id, 
            category.UserId, 
            category.Name, 
            category.Type
            );
        return Ok(response);

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