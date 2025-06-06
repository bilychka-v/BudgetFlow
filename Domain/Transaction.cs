namespace Domain;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}