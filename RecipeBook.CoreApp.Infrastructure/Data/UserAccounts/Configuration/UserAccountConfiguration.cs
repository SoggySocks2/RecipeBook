using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.CoreApp.Domain.UserAccounts;

namespace RecipeBook.CoreApp.Infrastructure.Data.UserAccounts.Configuration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccount");

            builder.Property(x => x.Firstname).HasMaxLength(30);
            builder.Property(x => x.Lastname).HasMaxLength(50);
            builder.Property(x => x.Username).HasMaxLength(50);
            builder.Property(x => x.Password).HasMaxLength(30);
            builder.Property(x => x.Role).HasMaxLength(50);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasKey(x => x.Id);
        }
    }
}
