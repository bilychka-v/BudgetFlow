namespace BudgetFlow.Dtos;

public record CreateTransactionDto(
    Guid Id,
    Guid UserId,
    DateTime Date,
    Decimal Amount
    );