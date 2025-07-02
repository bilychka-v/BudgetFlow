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
}