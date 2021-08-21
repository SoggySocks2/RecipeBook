using RecipeBook.CoreApp.Domain.Recipes;
using System.Collections.Generic;

namespace RecipeBook.CoreApp.Infrastructure.Data.Recipes.Seeds
{
    public static class RecipeSeed
    {
        public static List<Recipe> GetRecipes()
        {
            var recipes = new List<Recipe>();

            for (var recipe = 0; recipe < 50; recipe++)
            {
                var ingredients = new List<Ingredient>();
                for (short ingredient = 0; ingredient < 3; ingredient++)
                {
                    ingredients.Add(new Ingredient($"Recipe {recipe} - Ingredient {ingredient}", $"Recipe {recipe} - Unit Of Measure {ingredient}", (short)(ingredient + 1)));
                }
                recipes.Add(new Recipe($"Recipe {recipe}", $"Description {recipe}", $"Note {recipe}", recipe, ingredients));
            }

            return recipes;
        }
    }
}
