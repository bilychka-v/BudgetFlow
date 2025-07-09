using Application.Filters;
using Domain;

namespace Application.Extensions;

public static class TransactionExtension
{
    public static IQueryable<Transaction> Filter(this IQueryable<Transaction> query, TransactionFilter filter)
    {
        if(!string.IsNullOrEmpty(filter.CategoryName))
            query = query.Where(x => x.Category.Name == filter.CategoryName);
        
        if(filter.From != null)
            query = query.Where(x => x.Date.ToUniversalTime() >= filter.From.Value.ToUniversalTime());
        
        if(filter.To != null)
            query = query.Where(x => x.Date.ToUniversalTime() <= filter.To.Value.ToUniversalTime());
        
        if(filter.Min != null)
            query = query.Where(x=> x.Amount >= filter.Min.Value);
        
        if(filter.Max != null)
            query = query.Where(x=> x.Amount <= filter.Max.Value);
        
        return query;
            
    }
    
    
}