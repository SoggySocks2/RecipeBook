using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.Account;
using RecipeBook.CoreApp.Infrastructure.Data.Account.Configuration;

namespace RecipeBook.CoreApp.Infrastructure.Data
{
    public class CoreDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccountConfiguration).Assembly);
        }
    }
}
