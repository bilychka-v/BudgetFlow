using System.ComponentModel.DataAnnotations;
using Domain;

namespace BudgetFlow.Dtos;

public record CreateCategoryDto(
    Guid UserId,
    
    [Required]
    string Name,
    
    [Required]
    CategoryType Type
    );