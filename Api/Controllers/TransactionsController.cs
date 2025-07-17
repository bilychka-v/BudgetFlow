using Application.Exceptions;
using Application.Filters;
using Application.Interfaces;
using BudgetFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    
    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] TransactionFilter filter)
    {
        var transactions = await _transactionService.GetAllTransactionsAsync(filter);
        return Ok(transactions);
    }

    [HttpPost]
    public async Task <IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // TODO any conditionals in controller
        if(transactionDto.Amount <= 0)
            return BadRequest("Amount cannot be negative or equal to zero");
        
        if(transactionDto.Date > DateTime.Now)
            return BadRequest("Date cannot be later than today");
        
        
        var createdTransactionsDto = await _transactionService.AddTransactionAsync(transactionDto.UserId, transactionDto);
        // Status 201 Created
        return CreatedAtAction(nameof(GetTransactions), new { userId = createdTransactionsDto.UserId }, createdTransactionsDto);

    }

    [HttpPut]
    [Route("{id}")]
    public async Task <IActionResult> UpdateTransaction([FromRoute] Guid id, [FromBody] UpdateTransactionDto transaction)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        var transactionUpdate = await _transactionService.UpdateTransactionAsync(id, transaction);
        
        // todo to service
        if (transactionUpdate == null)
        {
            throw new NotFoundException("Transaction not found");
        }

        return Ok(transactionUpdate);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task <IActionResult> DeleteTransaction([FromRoute] Guid id)
    {
        await  _transactionService.DeleteTransactionAsync(id);
        return NoContent();
    }
}