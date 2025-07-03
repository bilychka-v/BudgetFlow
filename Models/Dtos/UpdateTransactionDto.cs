namespace BudgetFlow.Dtos;

public record UpdateTransactionDto(
    DateTime Date,
    decimal Amount
    );