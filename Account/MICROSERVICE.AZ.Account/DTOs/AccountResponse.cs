namespace MICROSERVICES.AZ.Account.DTOs;

public class AccountResponse
{
    public int IdAccount { get; set; }
    public decimal TotalAmount { get; set; }
    public int IdCustomer { get; set; }
    public string? FullName { get; set; }

}

