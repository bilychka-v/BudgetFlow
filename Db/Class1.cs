using Domain;
using Microsoft.EntityFrameworkCore;

namespace Db;

public class DataContext : DbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Transaction> Transactions { get; set; }
}