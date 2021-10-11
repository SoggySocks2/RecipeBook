using FluentAssertions;
using RecipeBook.CoreAppTests.Shared.Recipes.Builders;
using RecipeBook.SharedKernel.Exceptions;
using System;
using Xunit;

namespace RecipeBook.CoreAppTests.Domain.UnitTests.Recipes
{
    public class IngredientTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_MissingName_ThrowsEmptyInputException(string name)
        {
            Action act = () => new IngredientBuilder().WithTestValues().WithName(name).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("name is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_MissingUnitOfMeasure_ThrowsEmptyInputException(string unitOfMeasure)
        {
            Action act = () => new IngredientBuilder().WithTestValues().WithUnitOfMeasure(unitOfMeasure).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("unitOfMeasure is required");
        }

        [Fact]
        public void Constructor_InvalidQty_ThrowsEmptyInputException()
        {
            Action act = () => new IngredientBuilder().WithTestValues().WithQty(0).Build();
            act.Should().Throw<EmptyInputException>().WithMessage("qty is required");
        }

        [Fact]
        public void Constructor_IsValid_ConstructsOK()
        {
            Action act = () => new IngredientBuilder().WithTestValues().Build();
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void UpdateUnitOfMeasure_MissingUnitOfMeasure_ThrowsEmptyInputException(string unitOfMeasure)
        {
            var ingredient = new IngredientBuilder().WithTestValues().Build();
            Action act = () => ingredient.UpdateUnitOfMeasure(unitOfMeasure);
            act.Should().Throw<EmptyInputException>().WithMessage("unitOfMeasure is required");
        }

        [Fact]
        public void UpdateUnitOfMeasure_ValidUnitOfMeasure_UpdatesUnitOfMeasure()
        {
            var testValue1 = Guid.NewGuid().ToString();
            var testValue2 = Guid.NewGuid().ToString();

            var ingredient = new IngredientBuilder().WithTestValues().WithUnitOfMeasure(testValue1).Build();
            ingredient.UnitOfMeasure.Should().Be(testValue1);

            ingredient.UpdateUnitOfMeasure(testValue2);
            ingredient.UnitOfMeasure.Should().Be(testValue2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateQty_InvalidQty_ThrowsEmptyInputException(short qty)
        {
            var ingredient = new IngredientBuilder().WithTestValues().Build();
            Action act = () => ingredient.UpdateQty(qty);
            act.Should().Throw<EmptyInputException>().WithMessage("qty is required");
        }

        [Fact]
        public void UpdateQty_ValidQty_UpdatesQty()
        {
            var testValue1 = (short)5;
            var testValue2 = (short)10;

            var ingredient = new IngredientBuilder().WithTestValues().WithQty(testValue1).Build();
            ingredient.Qty.Should().Be(testValue1);

            ingredient.UpdateQty(testValue2);
            ingredient.Qty.Should().Be(testValue2);
        }
    }
}
