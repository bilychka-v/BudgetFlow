using System.Globalization;
using Application.Exceptions;
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
        // Status 201 Created
        return CreatedAtAction(nameof(GetTransactions), new { userId = createdTransactionsDto.UserId }, createdTransactionsDto);

    }

    [HttpPut]
    [Route("{userId}/{id}")]
    public async Task <IActionResult> UpdateTransaction([FromRoute] Guid userId, [FromRoute] Guid id, [FromBody] UpdateTransactionDto transaction)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        var transactionUpdate = await _transactionService.UpdateTransactionAsync(userId, id, transaction);
        
        if (transactionUpdate == null)
        {
            throw new NotFoundException("Transaction not found");
        }

        return Ok(transactionUpdate);
    }

    [HttpDelete]
    [Route("{userId}/{id}")]
    public async Task <IActionResult> DeleteTransaction([FromRoute] Guid userId, [FromRoute] Guid id)
    {
        await  _transactionService.DeleteTransactionAsync(userId, id);
        return NoContent();
    }
}