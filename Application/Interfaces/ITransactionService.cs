using Application.Filters;
using BudgetFlow.Dtos;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(TransactionFilter filter);
    Task<TransactionDto> GetTransactionAsync(Guid userId, Guid id);
    Task<TransactionDto> AddTransactionAsync(Guid userId, CreateTransactionDto transaction);
    Task<TransactionDto> UpdateTransactionAsync(Guid id, UpdateTransactionDto transaction);
    Task DeleteTransactionAsync(Guid id);
}