namespace MICROSERVICES.AZ.Account.DTOs;
public class TransactionResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string? Type { get; set; }
    public string? CreationDate { get; set; }
    public int AccountId { get; set; }
}

