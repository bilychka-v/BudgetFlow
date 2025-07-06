using Application.Interfaces;
using Db;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class CategoryRepository:ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category>> GetCategoriesAsync(Guid userId)
    {
        return await _context.Categories
            .Where(c=> c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Category> GetCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
         await _context.Categories.AddAsync(category);
         await _context.SaveChangesAsync();
         
         return category;
    }
}