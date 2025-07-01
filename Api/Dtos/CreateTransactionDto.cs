namespace BudgetFlow.Dtos;

public record CreateTransactionDto(
    Guid UserId,
    DateTime Date,
    Decimal Amount
    );