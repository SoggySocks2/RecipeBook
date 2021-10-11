using RecipeBook.SharedKernel.BaseClasses;
using RecipeBook.SharedKernel.Contracts;
using RecipeBook.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.CoreApp.Domain.Recipes
{
    public class Recipe : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Note { get; private set; }
        public decimal? Score { get; private set; }

        public IEnumerable<Ingredient> Ingredients => _ingredients.AsEnumerable();
        private readonly List<Ingredient> _ingredients = new();

        private Recipe() { }

        public Recipe(string name, string description, string note, decimal? score, IEnumerable<Ingredient> ingredients = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EmptyInputException($"{nameof(name)} is required");

            Name = name;
            UpdateDescription(description);
            UpdateNote(note);
            UpdateScore(score);

            if (ingredients != null)
            {
                foreach (var ingredient in ingredients)
                {
                    AddIngredient(ingredient.Name, ingredient.UnitOfMeasure, ingredient.Qty);
                }
            }
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new EmptyInputException($"{nameof(description)} is required");

            if (!description.Equals(Description, StringComparison.Ordinal))
            {
                Description = description;
            }
        }

        public void UpdateNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note)) throw new EmptyInputException($"{nameof(note)} is required");

            if (!note.Equals(Note, StringComparison.Ordinal))
            {
                Note = note;
            }
        }

        public void UpdateScore(decimal? score)
        {
            if (score != null && score < 0) throw new EmptyInputException($"{nameof(score)} is required");

            if (!score.Equals(Score))
            {
                Score = score;
            }
        }

        public void AddIngredient(string name, string unitOfMeasure, short qty)
        {
            if (_ingredients.Exists(i => i.Name == name)) throw new ExistsException($"{nameof(name)} already exists");

            var ingredient = new Ingredient(name, unitOfMeasure, qty);

            _ingredients.Add(ingredient);
        }

        public Ingredient UpdateIngredient(Guid id, string unitOfMeasure, short qty)
        {
            if (id == Guid.Empty) throw new EmptyInputException($"{nameof(id)} is required");

            var ingredient = _ingredients.FirstOrDefault(i => i.Id.Equals(id));
            if (ingredient == null)
            {
                throw new NotFoundException($"Id {id} not found");
            }

            ingredient.UpdateUnitOfMeasure(unitOfMeasure);
            ingredient.UpdateQty(qty);

            return ingredient;
        }

        public void RemoveIngredient(Guid id)
        {
            var ingredient = _ingredients.Find(i => i.Id.Equals(id));
            if (ingredient != null)
            {
                _ingredients.Remove(ingredient);
            }
        }
    }
}
