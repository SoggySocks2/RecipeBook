using RecipeBook.CoreApp.Domain.Recipes;
using System;

namespace RecipeBook.CoreAppTests.Shared.Recipes.Builders
{
    public class IngredientBuilder
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string UnitOfMeasure { get; set; }
        private short Qty { get; set; }

        public IngredientBuilder WithTestValues()
        {
            Id = Guid.NewGuid();
            Name = Guid.NewGuid().ToString();
            UnitOfMeasure = Guid.NewGuid().ToString();
            Qty = 1;
            return this;
        }
        public IngredientBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }
        public IngredientBuilder WithName(string name)
        {
            Name = name;
            return this;
        }
        public IngredientBuilder WithUnitOfMeasure(string unitOfMeasure)
        {
            UnitOfMeasure = unitOfMeasure;
            return this;
        }
        public IngredientBuilder WithQty(short qty)
        {
            Qty = qty;
            return this;
        }
        public Ingredient Build()
        {
            var ingredient = new Ingredient(Name, UnitOfMeasure, Qty);
            if (Id != Guid.Empty) ingredient.GetType().GetProperty("Id").SetValue(ingredient, Id);

            return ingredient;
        }
    }
}
