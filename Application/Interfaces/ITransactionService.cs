using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId);
}