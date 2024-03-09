using Microsoft.EntityFrameworkCore;

namespace MICROSERVICES.AZ.Account.Repositories.Contexts;
public class ContextDatabase : DbContext
{
    public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
    {
    }

    public DbSet<Models.Account>? Accounts { get; set; }
    public DbSet<Models.Customer>? Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Account>().ToTable("Account");
        modelBuilder.Entity<Models.Customer>().ToTable("Customer");
    }
}

