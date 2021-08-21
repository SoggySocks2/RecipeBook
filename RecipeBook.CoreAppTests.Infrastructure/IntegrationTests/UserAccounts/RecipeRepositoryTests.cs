using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RecipeBook.CoreApp.Domain.Recipes;
using RecipeBook.CoreAppTests.Shared.General;
using RecipeBook.CoreAppTests.Shared.Recipes.Builders;
using RecipeBook.CoreAppTests.Shared.UserAccounts.Builders;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook.CoreAppTests.Infrastructure.IntegrationTests.UserAccounts
{
    public class RecipeRepositoryTests
    {
        /// <summary>
        /// Add a single recipe without any ingredients
        /// </summary>
        [Fact]
        public async Task AddAsync_RecipeWithoutIngedients_AddOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var newRecipe = new RecipeBuilder().WithTestValues().WithIngredients(null).Build();

            var recipe = await repo.AddAsync(newRecipe, CancellationToken.None);

            recipe.Name.Should().BeEquivalentTo(newRecipe.Name);
            recipe.Description.Should().BeEquivalentTo(newRecipe.Description);
            recipe.Note.Should().BeEquivalentTo(newRecipe.Note);
            recipe.Score.Should().Be(newRecipe.Score);
            recipe.Ingredients.Should().HaveCount(0);
            recipe.CreatedBy.Should().NotBe(Guid.Empty);
            recipe.ModifiedBy.Should().NotBe(Guid.Empty);
            recipe.IsDeleted.Should().BeFalse();
        }

        /// <summary>
        /// Add a single recipe with ingredients
        /// </summary>
        [Fact]
        public async Task AddAsync_RecipeWithIngedients_AddOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();
            var ingredients = new List<Ingredient>()
            {
                new IngredientBuilder().WithTestValues().WithId(Guid.NewGuid()).Build(),
                new IngredientBuilder().WithTestValues().WithId(Guid.NewGuid()).Build(),
                new IngredientBuilder().WithTestValues().WithId(Guid.NewGuid()).Build()
            };

            var newRecipe = new RecipeBuilder().WithTestValues().WithIngredients(ingredients).Build();

            var recipe = await repo.AddAsync(newRecipe, CancellationToken.None);

            recipe.Name.Should().BeEquivalentTo(newRecipe.Name);
            recipe.Description.Should().BeEquivalentTo(newRecipe.Description);
            recipe.Note.Should().BeEquivalentTo(newRecipe.Note);
            recipe.Score.Should().Be(newRecipe.Score);
            recipe.Ingredients.Should().HaveCount(ingredients.Count);
            recipe.CreatedBy.Should().NotBe(Guid.Empty);
            recipe.ModifiedBy.Should().NotBe(Guid.Empty);
            recipe.IsDeleted.Should().BeFalse();
        }

        [Fact]
        public void DeleteAsync_RecipeNotExists_DeletesOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();
            var recipe = new RecipeBuilder().WithTestValues().Build();

            Func<Task> func = async () =>
            {
                await repo.DeleteAsync(recipe, CancellationToken.None);
            };

            func.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task DeleteAsync_RecipeExists_DeletesOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var newRecipe = new RecipeBuilder().WithTestValues().Build();
            var recipe = await repo.AddAsync(newRecipe, CancellationToken.None);
            recipe.Should().NotBeNull();
            recipe.Id.Should().NotBe(Guid.Empty);

            Func<Task> func = async () =>
            {
                await repo.DeleteAsync(recipe, CancellationToken.None);
            };

            func.Should().NotThrow();

            var existingRecipe = await repo.GetByIdAsync(recipe.Id, CancellationToken.None);
            existingRecipe.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_IdNotExists_ReturnsOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var existingRecipe = await repo.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
            existingRecipe.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_IdExists_ReturnsOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var newRecipe = new RecipeBuilder().WithTestValues().Build();
            var recipe = await repo.AddAsync(newRecipe, CancellationToken.None);

            var existingRecipe = await repo.GetByIdAsync(recipe.Id, CancellationToken.None);

            existingRecipe.Should().NotBeNull();
            existingRecipe.Id.Should().NotBe(Guid.Empty);
            existingRecipe.Name.Should().Be(newRecipe.Name);
        }

        [Fact]
        public async Task GetListAsync_NoneData_ReturnsOk()
        {
            var dbContext = new CoreDbContextBuilder().WithTestValues(Guid.NewGuid().ToString()).Build();
            var repo = new RecipeRepositoryBuilder().WithTestValues().WithDbContext(dbContext).Build();
            var paginationFilter = new PaginationFilterBuilder().WithTestValues().Build();

            var recipes = await repo.GetListAsync(paginationFilter, CancellationToken.None);
            recipes.Data.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetListAsync_ExistingData_ReturnsOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var count = 5;
            for(var i = 0; i < count; i++)
            {
                var newRecipe = new RecipeBuilder().WithTestValues().WithId(Guid.Empty).Build();
                _ = await repo.AddAsync(newRecipe, CancellationToken.None);
            }

            var paginationFilter = new PaginationFilterBuilder().WithTestValues().WithPageSize(count).WithPage(1).Build();

            var recipes = await repo.GetListAsync(paginationFilter, CancellationToken.None);
            recipes.Data.Count.Should().Be(count);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOk()
        {
            var repo = new RecipeRepositoryBuilder().WithTestValues().Build();

            var newRecipe = new RecipeBuilder().WithTestValues().Build();
            var recipe = await repo.AddAsync(newRecipe, CancellationToken.None);

            var existingRecipe = await repo.GetByIdAsync(recipe.Id, CancellationToken.None);

            var newDescription = Guid.NewGuid().ToString();
            existingRecipe.UpdateDescription(newDescription);
            var updatedRecipe = await repo.UpdateAsync(existingRecipe, CancellationToken.None);
            updatedRecipe.Description.Should().Be(newDescription);

            var loadedRecipe = await repo.GetByIdAsync(recipe.Id, CancellationToken.None);
            loadedRecipe.Description.Should().Be(newDescription);
        }
    }
}
