using Domain;

namespace BudgetFlow.Dtos;

public record UpdateCategoryDto(
    string Name,
    CategoryType Type);