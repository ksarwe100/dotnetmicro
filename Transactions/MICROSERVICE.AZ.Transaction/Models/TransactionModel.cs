using System.ComponentModel.DataAnnotations.Schema;

namespace MICROSERVICE.AZ.Transaction.Models;

[Table("transaction")]
public class TransactionModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("amount")]
    public decimal Amount { get; set; }
    [Column("type")]
    public string? Type { get; set; }
    [Column("creationdate")]
    public string? CreationDate { get; set; }
    [Column("accountid")]
    public int AccountId { get; set; }
}


