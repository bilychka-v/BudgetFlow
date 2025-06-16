using Domain;
using Microsoft.EntityFrameworkCore;

namespace Db;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Username=postgres;Password=123456789;Host=localhost;Port=5432;Database=myBd;");
        }
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}