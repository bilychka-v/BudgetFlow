using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryAsync(Guid categoryId);
    Task<CategoryDto> AddCategoryAsync(CreateCategoryDto category);
    Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto category);
    Task DeleteCategoryAsync(Guid categoryId);
}