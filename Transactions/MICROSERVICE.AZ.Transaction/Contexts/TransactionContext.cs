using Microsoft.EntityFrameworkCore;

namespace MICROSERVICE.AZ.Transaction.Contexts;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
    {
    }

    public DbSet<Models.TransactionModel>? Transaction { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.TransactionModel>().ToTable("Transaction");
    }
}

