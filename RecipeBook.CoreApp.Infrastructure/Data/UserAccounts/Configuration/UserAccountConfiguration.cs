using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts.Configuration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable(nameof(UserAccount));

            // User account has a person value object
            builder.OwnsOne(x => x.Person, cb =>
            {
                cb.Property(p => p.FirstName).HasColumnName("FirstName");
                cb.Property(p => p.LastName).HasColumnName("LastName");
            });

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasKey(x => x.Id);
        }
    }
}
