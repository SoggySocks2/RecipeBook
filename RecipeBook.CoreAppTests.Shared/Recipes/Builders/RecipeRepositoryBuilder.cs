using RecipeBook.CoreApp.Infrastructure.Data;
using RecipeBook.CoreApp.Infrastructure.Data.Recipes;
using RecipeBook.CoreAppTests.Shared.General;

namespace RecipeBook.CoreAppTests.Shared.Recipes.Builders
{
    public class RecipeRepositoryBuilder
    {
        private CoreDbContext DbContext;

        public RecipeRepositoryBuilder WithTestValues()
        {
            DbContext = new CoreDbContextBuilder().WithTestValues().Build();
            return this;
        }
        public RecipeRepositoryBuilder WithDbContext(CoreDbContext dbContext)
        {
            DbContext = dbContext;
            return this;
        }
        public RecipeRepository Build()
        {
            return new RecipeRepository(DbContext);
        }
    }
}
