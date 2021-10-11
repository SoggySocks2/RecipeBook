using FluentAssertions;
using RecipeBook.CoreApp.Domain.Recipes;
using RecipeBook.CoreAppTests.Shared.Recipes.Builders;
using RecipeBook.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RecipeBook.CoreAppTests.Domain.UnitTests.Recipes
{
    public class RecipeTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_MissingName_ThrowsEmptyInputException(string name)
        {
            Action act = () => new RecipeBuilder().WithTestValues().WithName(name).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_MissingDescription_ThrowsEmptyInputException(string description)
        {
            Action act = () => new RecipeBuilder().WithTestValues().WithDescription(description).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("description is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_MissingNote_ThrowsEmptyInputException(string note)
        {
            Action act = () => new RecipeBuilder().WithTestValues().WithNote(note).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("note is required");
        }

        [Fact]
        public void Constructor_InvalidScore_ThrowsEmptyInputException()
        {
            Action act = () => new RecipeBuilder().WithTestValues().WithScore(-1).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("score is required");
        }

        [Fact]
        public void Constructor_IsValidWithIngredients_ConstructsOK()
        {
            Action act = () => new RecipeBuilder().WithTestValues().Build();
            act.Should().NotThrow();
        }

        [Fact]
        public void Constructor_IsValidWithoutIngredients_ConstructsOK()
        {
            Action act = () => new RecipeBuilder().WithTestValues().WithIngredients(null).Build();
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateDescription_MissingDescription_ThrowsEmptyInputException(string description)
        {
            var recipe = new RecipeBuilder().WithTestValues().Build();
            Action act = () => recipe.UpdateDescription(description);
            act.Should().Throw<EmptyInputException>().WithMessage("description is required");
        }

        [Fact]
        public void UpdateDescription_ValidDescription_UpdatesDescription()
        {
            var testValue1 = Guid.NewGuid().ToString();
            var testValue2 = Guid.NewGuid().ToString();

            var recipe = new RecipeBuilder().WithTestValues().WithDescription(testValue1).Build();
            recipe.Description.Should().Be(testValue1);

            recipe.UpdateDescription(testValue2);
            recipe.Description.Should().Be(testValue2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateNote_MissingNote_ThrowsEmptyInputException(string note)
        {
            var recipe = new RecipeBuilder().WithTestValues().Build();
            Action act = () => recipe.UpdateNote(note);
            act.Should().Throw<EmptyInputException>().WithMessage("note is required");
        }

        [Fact]
        public void UpdateNote_ValidNote_UpdatesNote()
        {
            var testValue1 = Guid.NewGuid().ToString();
            var testValue2 = Guid.NewGuid().ToString();

            var recipe = new RecipeBuilder().WithTestValues().WithNote(testValue1).Build();
            recipe.Note.Should().Be(testValue1);

            recipe.UpdateNote(testValue2);
            recipe.Note.Should().Be(testValue2);
        }

        [Fact]
        public void UpdateScore_InvalidScore_ThrowsEmptyInputException()
        {
            var recipe = new RecipeBuilder().WithTestValues().Build();
            Action act = () => recipe.UpdateScore(-1);
            act.Should().Throw<EmptyInputException>().WithMessage("score is required");
        }

        [Fact]
        public void UpdateScore_ValidScore_UpdatesScore()
        {
            var testValue1 = (decimal?)5;
            var testValue2 = (decimal?)10;

            var recipe = new RecipeBuilder().WithTestValues().WithScore(testValue1).Build();
            recipe.Score.Should().Be(testValue1);

            recipe.UpdateScore(testValue2);
            recipe.Score.Should().Be(testValue2);
        }

        [Fact]
        public void AddIngredient_AlreadyExists_ThrowsExistsException()
        {
            var ingredientName = Guid.NewGuid().ToString();

            var ingredient = new IngredientBuilder().WithTestValues().WithName(ingredientName).Build();
            var recipe = new RecipeBuilder().WithTestValues().WithIngredients(new List<Ingredient>() { ingredient }).Build();

            Action act = () => recipe.AddIngredient(ingredientName, Guid.NewGuid().ToString(), 1);
            act.Should().Throw<ExistsException>().WithMessage("name already exists");
        }

        [Fact]
        public void AddIngredient_NotAlreadyExists_AddsIngredient()
        {
            var ingredient = new IngredientBuilder().WithTestValues().Build();
            var recipe = new RecipeBuilder().WithTestValues().WithIngredients(new List<Ingredient>() { ingredient }).Build();

            recipe.AddIngredient(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1);
            ((ICollection<Ingredient>)recipe.Ingredients).Count.Should().Be(2);
        }

        [Fact]
        public void UpdateIngredient_EmptyId_ThrowsEmptyInputException()
        {
            var recipe = new RecipeBuilder().WithTestValues().Build();

            Action act = () => recipe.UpdateIngredient(Guid.Empty, Guid.NewGuid().ToString(), 1);
            act.Should().Throw<EmptyInputException>().WithMessage("id is required");
        }

        [Fact]
        public void UpdateIngredient_IdNotExists_ThrowsNotFoundException()
        {
            var recipe = new RecipeBuilder().WithTestValues().WithIngredients(null).Build();
            var id = Guid.NewGuid();
            var ingredient = new IngredientBuilder().WithTestValues().WithId(id).Build();

            Action act = () => recipe.UpdateIngredient(id, Guid.NewGuid().ToString(), 1);
            act.Should().Throw<NotFoundException>().WithMessage($"Id {id} not found");
        }

        [Fact]
        public void UpdateIngredient_IdExists_UpdatesIngredient()
        {
            var recipe = new RecipeBuilder().WithTestValues().Build();

            var id = ((ICollection<Ingredient>)recipe.Ingredients).FirstOrDefault().Id;
            var unitOfMeasure = Guid.NewGuid().ToString();
            var qty = (short)2;
            recipe.UpdateIngredient(id, unitOfMeasure, qty);

            var updateIngredient = recipe.Ingredients.FirstOrDefault(i => i.Id.Equals(id));
            updateIngredient.Should().NotBeNull();
            updateIngredient.UnitOfMeasure.Should().Be(unitOfMeasure);
            updateIngredient.Qty.Should().Be(qty);
        }


        [Fact]
        public void RemoveIngredient_IdExists_RemovesIngredient()
        {
            var ingredient = new IngredientBuilder().WithTestValues().Build();
            var recipe = new RecipeBuilder().WithTestValues().WithIngredients(new List<Ingredient>() { ingredient }).Build();
            ((ICollection<Ingredient>)recipe.Ingredients).Count.Should().Be(1);

            var id = ((ICollection<Ingredient>)recipe.Ingredients).FirstOrDefault().Id;
            recipe.RemoveIngredient(id);
            ((ICollection<Ingredient>)recipe.Ingredients).Count.Should().Be(0);
        }


        [Fact]
        public void RemoveIngredient_IdNotExists_IsOk()
        {
            var recipe = new RecipeBuilder().WithTestValues().WithIngredients(null).Build();

            Action act = () => recipe.RemoveIngredient(Guid.NewGuid());
            act.Should().NotThrow();
        }
    }
}
