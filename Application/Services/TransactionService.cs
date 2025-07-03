using Application.Interfaces;
using BudgetFlow.Dtos;
using Domain;


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
        if (!transactions.Any())
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

    public async Task<TransactionDto> GetTransactionAsync(Guid userId, Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        if(transaction == null || transaction.UserId != userId)
            throw new Exception("Invalid transaction");
        
        var transactionDto = new TransactionDto
        (
            Id: id, 
            UserId: userId, 
            Date: transaction.Date, 
            Amount: transaction.Amount
        );
        
        return transactionDto;
    }

    public async Task<TransactionDto> AddTransactionAsync(Guid userId, TransactionDto transactionDto)
    {
        var transaction = new Transaction
        {
            Id = new Guid(), 
            UserId = userId, 
            Date = transactionDto.Date, 
            Amount = transactionDto.Amount
        };
        
        await _repository.AddAsync(transaction);
       
        var newTransactionDto = new TransactionDto
            (
                transaction.Id, 
                transaction.UserId, 
                transaction.Date, 
                transaction.Amount);
        
        return newTransactionDto;
    }
}