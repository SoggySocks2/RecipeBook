using RecipeBook.CoreApp.Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.CoreAppTests.Shared.Recipes.Builders
{
    public class RecipeBuilder
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private string Note { get; set; }
        private decimal? Score { get; set; }
        private List<Ingredient> Ingredients { get; set; }

        public RecipeBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Name = Guid.NewGuid().ToString();
            Description = Guid.NewGuid().ToString();
            Note = Guid.NewGuid().ToString();
            Score = (decimal?)6.5;
            Ingredients = new()
            {
                new IngredientBuilder().WithTestValues().Build(),
                new IngredientBuilder().WithTestValues().Build(),
                new IngredientBuilder().WithTestValues().Build()
            };
            return this;
        }
        public RecipeBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public RecipeBuilder WithName(string name)
        {
            Name = name;
            return this;
        }
        public RecipeBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }
        public RecipeBuilder WithNote(string note)
        {
            Note = note;
            return this;
        }
        public RecipeBuilder WithScore(decimal? score)
        {
            Score = score;
            return this;
        }
        public RecipeBuilder WithIngredients(List<Ingredient> ingredients)
        {
            Ingredients = ingredients;
            return this;
        }
        public Recipe Build()
        {
            var recipe = new Recipe(Name, Description, Note, Score);
            if (Id != Guid.Empty) recipe.GetType().GetProperty("Id").SetValue(recipe, Id);

            if (Ingredients != null)
            {
                foreach (var ingredient in Ingredients.Where(i => i.Id != Guid.Empty))
                {
                    recipe.AddIngredient(ingredient.Name, ingredient.UnitOfMeasure, ingredient.Qty);

                    // Locate existing ingredient and set the id
                    recipe.Ingredients.FirstOrDefault(i => i.Name.Equals(ingredient.Name)).GetType().GetProperty("Id").SetValue(recipe.Ingredients.FirstOrDefault(i => i.Name.Equals(ingredient.Name)), ingredient.Id);
                }
            }

            return recipe;
        }
    }
}
