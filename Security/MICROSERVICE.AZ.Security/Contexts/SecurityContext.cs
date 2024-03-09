using Microsoft.EntityFrameworkCore;

namespace MICROSERVICES.AZ.Security.Repositories
{
    public class SecurityContext : DbContext
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        {
        }

        public DbSet<Models.IdentityModel> UserAccess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.IdentityModel>().ToTable("UserAccess");
        }
    }
}
