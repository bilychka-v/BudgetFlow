using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId);
    Task<CategoryDto> GetCategoryAsync(Guid userId, Guid categoryId);
    Task<CategoryDto> AddCategoryAsync(Guid userId, CreateCategoryDto category);
    Task<CategoryDto> UpdateCategoryAsync(Guid userId, Guid categoryId, UpdateCategoryDto category);
    Task DeleteCategoryAsync(Guid userId, Guid categoryId);
}