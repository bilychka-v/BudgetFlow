namespace BudgetFlow.Dtos;

public record TransactionDto(
    Guid Id,
    Guid UserId,
    DateTime Date,
    decimal Amount,
    string CategoryName,
    string CategoryType
    );