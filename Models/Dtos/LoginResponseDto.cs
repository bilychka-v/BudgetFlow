namespace BudgetFlow.Dtos;

public record LoginResponseDto
    (
        string Token,
        string Username,
        string RefreshToken
    );