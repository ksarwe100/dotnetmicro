namespace MICROSERVICE.AZ.Movement.DTOs;
public class MovementResponse
{
    public int IdTransaction { get; set; }
    public decimal Amount { get; set; }
    public string? Type { get; set; }
    public string? CreationDate { get; set; }
    public int AccountId { get; set; }
}

