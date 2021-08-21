using RecipeBook.SharedKernel.Responses;
using RecipeBook.SharedKernel.SharedObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecipeBook.CoreApp.Domain.Recipes.Contracts
{
    public interface IRecipeRepository
    {
        Task<Recipe> AddAsync(Recipe recipe, CancellationToken cancellationToken);

        Task<Recipe> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<PagedResponse<List<Recipe>>> GetListAsync(PaginationFilter filter, CancellationToken cancellationToken);

        Task<Recipe> UpdateAsync(Recipe recipe, CancellationToken cancellationToken);

        Task DeleteAsync(Recipe recipe, CancellationToken cancellationToken);
    }
}
