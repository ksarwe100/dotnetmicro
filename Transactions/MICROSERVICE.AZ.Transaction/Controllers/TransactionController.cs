using MICROSERVICE.AZ.Transaction.Contexts;
using MICROSERVICE.AZ.Transaction.DTOs;
using MICROSERVICE.AZ.Transaction.Messages;
using MICROSERVICE.AZ.Transaction.Models;
using Microsoft.AspNetCore.Mvc;

namespace MICROSERVICE.AZ.Transaction.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly TransactionContext _transactionContext;
    private readonly IEventBus _eventBus;

    public TransactionController(TransactionContext transactionContext,
        IEventBus eventBus)
    {
        _transactionContext = transactionContext;
        _eventBus = eventBus;

    }

    [HttpPost("Deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
    {
        TransactionModel transactionModel = new()
        {
            AccountId = request.AccountId,
            Amount = request.Amount,
            Type = "Deposit",
            CreationDate = DateTime.Now.ToString()
        };
        _transactionContext.Add(transactionModel);
        await _transactionContext.SaveChangesAsync();

        await _eventBus.PublishMessage(transactionModel);

        return Ok(transactionModel);
    }

    [HttpPost("withdrawal")]
    public async Task<IActionResult> Withdrawal([FromBody] TransactionRequest request)
    {
        TransactionModel transactionModel = new()
        {
            AccountId = request.AccountId,
            Amount = request.Amount * -1,
            Type = "withdrawal",
            CreationDate = DateTime.Now.ToString()
        };
        _transactionContext.Add(transactionModel);
        await _transactionContext.SaveChangesAsync();

        await _eventBus.PublishMessage(transactionModel);

        return Ok(transactionModel);
    }

}

