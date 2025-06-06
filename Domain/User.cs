namespace Domain;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public decimal Currency  { get; set; }
    public decimal BalanceLimit { get; set; }
}