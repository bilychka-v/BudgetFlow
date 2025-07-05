using Application.Interfaces;
using BudgetFlow.Dtos;
using Db;
using Domain;


namespace Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly DataContext _context;
    public TransactionService(ITransactionRepository repository, DataContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(Guid userId)
    {
        var transactions = await _repository.GetAllAsync(userId);
        if (transactions is null)
            return Array.Empty<TransactionDto>();
        
        var transactionDtos = transactions.Select(t => new TransactionDto
        (
            Id : t.Id,
            UserId : t.UserId,
            Date : t.Date,
            Amount : t.Amount,
            CategoryName : t.Category.Name
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
            Amount: transaction.Amount,
            CategoryName: transaction.Category.Name
        );
        
        return transactionDto;
    }

    public async Task<TransactionDto> AddTransactionAsync(Guid userId, CreateTransactionDto transactionDto)
    {
        var category = await _context.Categories.FindAsync(transactionDto.CategoryId);
        
        var transaction = new Transaction
        {
            Id = new Guid(), 
            UserId = userId, 
            Date = transactionDto.Date, 
            Amount = transactionDto.Amount,
            CategoryId = transactionDto.CategoryId
        };
        
        await _repository.AddAsync(transaction);
       
        var newTransactionDto = new TransactionDto
            (
                transaction.Id, 
                transaction.UserId, 
                transaction.Date, 
                transaction.Amount,
                category.Name
            );
        
        return newTransactionDto;
    }

    public async Task<TransactionDto> UpdateTransactionAsync(Guid userId, Guid id, UpdateTransactionDto updateTransactionDto)
    {
        var transaction = await _repository.GetByIdAsync(id);
        
        var category = await _context.Categories.FindAsync(transaction.CategoryId);
        
        if(transaction == null || transaction.UserId != userId)
            throw new Exception("Invalid transaction");
        
        transaction.Date = updateTransactionDto.Date <= DateTime.Now ? updateTransactionDto.Date : throw new Exception("Invalid date");
        transaction.Amount = updateTransactionDto.Amount <= 0 ? updateTransactionDto.Amount : throw new Exception("Invalid amount");
        
        await _repository.UpdateAsync(transaction);

        var transactionNew = new TransactionDto
        (
            Id: transaction.Id,
            UserId: transaction.UserId,
            Date: transaction.Date,
            Amount: transaction.Amount,
            CategoryName: category.Name
        );
        
        return transactionNew;
    }

    public async Task DeleteTransactionAsync(Guid userId, Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        
        if(transaction == null || transaction.UserId != userId)
            throw new Exception("Invalid transaction");
        
        await _repository.DeleteAsync(id);
    }
}