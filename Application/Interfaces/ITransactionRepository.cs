using Application.Filters;
using Domain;

namespace Application.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync(Guid userId, TransactionFilter filter);
    Task<Transaction> GetByIdAsync(Guid id);
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    Task DeleteAsync(Guid id);
    
}