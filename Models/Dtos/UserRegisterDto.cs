namespace BudgetFlow.Dtos;

public record UserRegisterDto(
    Guid UserId,
    string UserName,
    string Email,
    string PasswordHash
    );