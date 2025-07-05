using Application.Interfaces;
using BudgetFlow.Dtos;
using Db;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ITransactionService _transactionService;
    
    public TransactionsController(DataContext context,  ITransactionService transactionService)
    {
        _context = context;
        _transactionService = transactionService;
    }
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetTransactions([FromRoute] Guid userId)
    {
        var transactions = await _transactionService.GetAllTransactionsAsync(userId);
        return Ok(transactions);
    }

    [HttpPost]
    public async Task <IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        if(transactionDto.Amount <= 0)
            return BadRequest("Amount cannot be negative or equal to zero");
        
        if(transactionDto.Date > DateTime.Now)
            return BadRequest("Date cannot be later than today");
        
        
        var createdTransactionsDto = await _transactionService.AddTransactionAsync(transactionDto.UserId, transactionDto);
        
        return Ok(createdTransactionsDto);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task <IActionResult> UpdateTransaction([FromRoute] Guid id, [FromBody] UpdateTransactionDto transaction)
    {
        var transactionUpdate = await _context.Transactions.Include(transaction => transaction.Category).FirstOrDefaultAsync(s=>s.Id==id);
        
        if (transactionUpdate == null)
        {
            return NotFound();
        }
        
        transactionUpdate.Amount = transaction.Amount > 0 ? transaction.Amount : transactionUpdate.Amount;
        transactionUpdate.Date = transaction.Date > DateTime.Now ? transaction.Date : transactionUpdate.Date;
        
        await _context.SaveChangesAsync();
        
        var response = new TransactionDto
        (
            transactionUpdate.Id, 
            transactionUpdate.UserId, 
            transactionUpdate.Date,
            transactionUpdate.Amount,
            transactionUpdate.Category.Name
        );

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task <IActionResult> DeleteTransaction([FromRoute] Guid id)
    {
        var transaction = await  _context.Transactions.FirstOrDefaultAsync(s => s.Id == id);
        if(transaction == null)
            return NotFound();
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}