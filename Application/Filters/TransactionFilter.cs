namespace Application.Filters;

public class TransactionFilter
{
    public string? CategoryName  { get; set; }
    public DateTime? From  { get; set; }
    public DateTime? To  { get; set; }
    public decimal? Min  { get; set; }
    public decimal? Max  { get; set; }
}