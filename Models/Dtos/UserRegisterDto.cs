namespace BudgetFlow.Dtos;

public record UserRegisterDto(
    string UserName,
    string Email,
    string Password
    );