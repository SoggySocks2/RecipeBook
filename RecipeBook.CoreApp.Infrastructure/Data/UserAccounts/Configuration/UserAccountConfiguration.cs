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

            builder.OwnsOne(x => x.Person, cb =>
            {
                cb.Property(p => p.FirstName).HasColumnName("FirstName");
                cb.Property(p => p.LastName).HasColumnName("LastName");
            });

            //builder.Property(x => x.Username).HasMaxLength(50);
            //builder.Property(x => x.Password).HasMaxLength(30);
            //builder.Property(x => x.Role).HasMaxLength(50);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasKey(x => x.Id);
        }
    }
}
