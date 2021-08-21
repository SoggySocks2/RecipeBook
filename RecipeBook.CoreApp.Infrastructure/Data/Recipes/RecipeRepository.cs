using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.Recipes;
using RecipeBook.CoreApp.Domain.Recipes.Contracts;
using RecipeBook.SharedKernel.Extensions;
using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Infrastructure.Data.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CoreDbContext _dbContext;

        public RecipeRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Recipe> AddAsync(Recipe recipe, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _dbContext.AddAsync(recipe, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return recipe;
        }

        public async Task DeleteAsync(Recipe recipe, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _dbContext.Recipes
                .Include(c => c.Ingredients)
                .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<PagedResponse<List<Recipe>>> GetListAsync(PaginationFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var count = await _dbContext.Recipes.CountAsync(cancellationToken);

            var data = await _dbContext.Recipes
                .ApplyOrderingAndPaging(filter, count)
                .Include(c => c.Ingredients)
                .ToListAsync(cancellationToken);

            var res = new PagedResponse<List<Recipe>>(data, new Pagination(filter, count));

            return new PagedResponse<List<Recipe>>(data, new Pagination(filter, count));
        }

        public async Task<Recipe> UpdateAsync(Recipe recipe, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _dbContext.Update(recipe);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return recipe;
        }
    }
}
