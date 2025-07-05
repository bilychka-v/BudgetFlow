using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId);
    Task<TransactionDto> GetTransactionAsync(Guid userId, Guid id);
    Task<TransactionDto> AddTransactionAsync(Guid userId, CreateTransactionDto transaction);
    Task<TransactionDto> UpdateTransactionAsync(Guid userId, Guid id, UpdateTransactionDto transaction);
    Task DeleteTransactionAsync(Guid userId, Guid id);
}