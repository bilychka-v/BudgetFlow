using Domain;

namespace Application.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync(Guid userId);
    Task<Transaction> GetByIdAsync(Guid id);
    Task<Transaction> AddAsync(Transaction transaction);
    Task<Transaction> UpdateAsync(Transaction transaction);
    // Task<Transaction> Delete(Guid id);
    
}