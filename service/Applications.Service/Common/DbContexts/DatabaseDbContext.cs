using Applications.Service.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Applications.Service.Common.DbContexts;

public class DatabaseDbContext : DbContext
{
    public DbSet<CurrentAccountEntity> CurrentAccounts { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Database");
    }
}