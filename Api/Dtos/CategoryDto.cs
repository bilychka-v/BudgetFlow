using Domain;

namespace BudgetFlow.Dtos;

public record CategoryDto(
    Guid Id,
    Guid UserId,
    string Name,
    CategoryType Type
    );