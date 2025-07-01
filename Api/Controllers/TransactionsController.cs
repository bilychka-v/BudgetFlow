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
    
    public TransactionsController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _context.Transactions.ToListAsync();
        var response = transactions.Select(t => new TransactionDto(
            Id: t.Id,
            UserId: t.UserId,
            Amount: t.Amount,
            Date: t.Date)
        ).ToList();
        return Ok(response);
    }

    [HttpPost]
    public async Task <IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        if(transactionDto.Amount <= 0)
            return BadRequest("Amount cannot be negative or equal to zero");

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            UserId = transactionDto.UserId,
            Amount = transactionDto.Amount,
            Date = transactionDto.Date
        };
        
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        
        var response = new TransactionDto
        (
            Id : transaction.Id, 
            UserId : transaction.UserId, 
            Amount : transaction.Amount, 
            Date : transaction.Date
        );
        
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task <IActionResult> UpdateTransaction([FromRoute] Guid id, [FromBody] UpdateTransactionDto transaction)
    {
        var transactionUpdate = await  _context.Transactions.FirstOrDefaultAsync(s=>s.Id==id);
        
        if (transactionUpdate == null)
        {
            return NotFound();
        }
        
        transactionUpdate.Amount = transaction.Amount > 0 ? transaction.Amount : transactionUpdate.Amount;
        transactionUpdate.Date = transaction.Date;
        
        await _context.SaveChangesAsync();
        
        var response = new TransactionDto
        (
            Id : transactionUpdate.Id, 
            UserId : transactionUpdate.UserId, 
            Amount : transactionUpdate.Amount, 
            Date : transactionUpdate.Date
        );

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task <IActionResult> DeleteTransaction([FromRoute] Guid id)
    {
        var transaction =await  _context.Transactions.FirstOrDefaultAsync(s => s.Id == id);
        if(transaction == null)
            return NotFound();
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
}