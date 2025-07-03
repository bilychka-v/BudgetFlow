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

    public async Task<IEnumerable<Transaction>> GetAllAsync(Guid userId)
    {
        return await _context.Transactions.Where(t=>t.UserId == userId).ToListAsync();
    }

    public async Task<Transaction?> GetByIdAsync(Guid transactionId)
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
}