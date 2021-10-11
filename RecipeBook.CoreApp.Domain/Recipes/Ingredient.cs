using RecipeBook.SharedKernel.BaseClasses;
using RecipeBook.SharedKernel.Exceptions;
using System;

namespace RecipeBook.CoreApp.Domain.Recipes
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public short Qty { get; private set; }
        public Recipe Recipe { get; private set; }

        private Ingredient() { }

        public Ingredient(string name, string unitOfMeasure, short qty)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EmptyInputException($"{nameof(name)} is required");

            Name = name;
            UpdateUnitOfMeasure(unitOfMeasure);
            UpdateQty(qty);
        }

        public void UpdateUnitOfMeasure(string unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(unitOfMeasure)) throw new EmptyInputException($"{nameof(unitOfMeasure)} is required");

            if (!unitOfMeasure.Equals(UnitOfMeasure, StringComparison.Ordinal))
            {
                UnitOfMeasure = unitOfMeasure;
            }
        }

        public void UpdateQty(short qty)
        {
            if (qty < 1) throw new EmptyInputException($"{nameof(qty)} is required");

            if (!qty.Equals(Qty))
            {
                Qty = qty;
            }
        }
    }
}
