using Application.Interfaces;
using BudgetFlow.Dtos;
using Domain;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(Guid userId)
    {
        var categories = await _categoryRepository.GetCategoriesAsync(userId);

        var categoriesDto = categories
            .Select(t => new CategoryDto
                (
                    t.Id, 
                    t.UserId, 
                    t.Name, t.Type
                )).ToList();
        
        return categoriesDto;
    }

    public async Task<CategoryDto> GetCategoryAsync(Guid userId, Guid id)
    {
        var category = await _categoryRepository.GetCategoryAsync(id);
        
        if(category == null || userId != category.UserId)
            throw new Exception("Category does not exist");
        
        var categoryDto = new CategoryDto
            (
                category.Id, 
                category.UserId, 
                category.Name, 
                category.Type
            );
        return categoryDto;
    }

    public async Task<CategoryDto> AddCategoryAsync(Guid userId, CreateCategoryDto createCategoryDto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            UserId = userId, 
            Name = createCategoryDto.Name,
            Type = createCategoryDto.Type
        };
        
        await _categoryRepository.AddCategoryAsync(category);
        
        var categoryDto = new CategoryDto
            (
                category.Id, 
                category.UserId, 
                category.Name, 
                category.Type
            );
        
        return categoryDto;
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid userId, Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _categoryRepository.GetCategoryAsync(categoryId);
        
        if(category == null || userId != category.UserId)
            throw new Exception("Category does not exist");
        
        category.Name = updateCategoryDto.Name;
        category.Type = updateCategoryDto.Type;
        
       await _categoryRepository.UpdateCategoryAsync(category);
       
       var categoryDto = new CategoryDto
           (
               Id: category.Id, 
               category.UserId, 
               category.Name, 
               category.Type
           );
       
       return categoryDto;
    }

    public async Task DeleteCategoryAsync(Guid userId, Guid categoryId)
    {
        var category = await _categoryRepository.GetCategoryAsync(categoryId);
        
        if(category == null || userId != category.UserId)
            throw new Exception("Category does not exist");
        
        await _categoryRepository.DeleteCategoryAsync(category.Id);
    }
}