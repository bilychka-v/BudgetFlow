using System.Runtime.InteropServices.JavaScript;
using Application.Interfaces;
using BudgetFlow.Dtos;
using System.Linq;


namespace Application.Services;

public class TransactionService:ITransactionService
{
    private readonly ITransactionRepository _repository;
    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId)
    {
        var transactions = await _repository.GetAllAsync(userId);
        if (transactions is null || !transactions.Any())
            return Array.Empty<TransactionDto>();
        
        var transactionDtos = transactions.Select(t => new TransactionDto
        (
            Id : t.Id,
            UserId : t.UserId,
            Date : t.Date,
            Amount : t.Amount
        )).ToList();
        
        return transactionDtos;
    }
}