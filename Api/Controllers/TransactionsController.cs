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
    [Route("/transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _context.Transactions.ToListAsync();
        return Ok(transactions);
    }

    [HttpPost]
    [Route("/transactions")]
    public IActionResult AddTransaction([FromBody] Transaction transaction)
    {
        if(transaction.Amount < 0)
            return BadRequest("Amount cannot be negative");
        
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    [Route("transactions/{id}")]
    public IActionResult UpdateTransaction([FromRoute] Guid id, [FromBody] Transaction transaction)
    {
        var transactionUpdate = _context.Transactions.FirstOrDefault(s=>s.Id==id);
        
        if (transactionUpdate== null)
        {
            return NotFound();
        }
        
        transactionUpdate.Amount = transaction.Amount > 0 ? transaction.Amount : transactionUpdate.Amount;
        transactionUpdate.Date = transaction.Date;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete]
    [Route("transactions/{id}")]
    public IActionResult DeleteTransaction([FromRoute] Guid id)
    {
        var transaction = _context.Transactions.FirstOrDefault(s => s.Id == id);
        if(transaction == null)
            return NotFound();
        _context.Transactions.Remove(transaction);
        _context.SaveChanges();
        return NoContent();
    }
    
}