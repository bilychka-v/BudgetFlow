using Application.Extensions;
using Application.Filters;
using Application.Interfaces;
using Db;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class TransactionRepository: ITransactionRepository
{
    private readonly DataContext _context;

    public TransactionRepository(DataContext context)
    {
        _context = context;
        
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync(Guid userId, TransactionFilter filter)
    {
        return await _context.Transactions
            .Where(t=>t.UserId == userId)
            .Include(t=>t.Category)
            .Filter(filter)
            .ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(Guid transactionId)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(t=> t.Id == transactionId);
        
        return transaction;
    }

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        
        return transaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task DeleteAsync(Guid id)
    {
        var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}