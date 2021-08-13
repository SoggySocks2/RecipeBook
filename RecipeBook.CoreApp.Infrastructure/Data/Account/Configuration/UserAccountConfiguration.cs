using RecipeBook.CoreApp.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecipeBook.CoreApp.Infrastructure.Data.Account.Configuration
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

            builder.HasKey(x => x.Id);
        }
    }
}
