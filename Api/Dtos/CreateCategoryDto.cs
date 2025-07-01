using Domain;

namespace BudgetFlow.Dtos;

public record CreateCategoryDto(
    Guid UserId,
    string Name,
    CategoryType Type
    );