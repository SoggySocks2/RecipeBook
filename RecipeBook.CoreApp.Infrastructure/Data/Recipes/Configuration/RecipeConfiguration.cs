using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.CoreApp.Domain.Recipes;

namespace RecipeBook.CoreApp.Infrastructure.Data.Recipes.Configuration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        /// <summary>
        /// Configure recipe table
        /// </summary>
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable(nameof(Recipe));

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Metadata.FindNavigation(nameof(Recipe.Ingredients)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(x => x.Id);
        }
    }
}
