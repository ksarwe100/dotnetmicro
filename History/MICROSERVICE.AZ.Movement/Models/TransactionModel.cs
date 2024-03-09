using MICROSERVICE.AZ.Movement.ConfigCollection;

namespace MICROSERVICE.AZ.Movement.Models;

[BsonCollection("transactionsCollection")]
public class TransactionModel : Document
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? Type { get; set; }
    public string? CreationDate { get; set; }
    public int AccountId { get; set; }
}

