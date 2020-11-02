using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace HomeTask5.Core.Tests
{
    public class AmountRecipeIngredientsControllerTests : BaseTests
    {
        private readonly AmountRecipeIngredientsController _amountRecipeIngredientsController;
        private readonly AmountIngredient _amountIngredient;
        public AmountRecipeIngredientsControllerTests()
        {
            _amountRecipeIngredientsController = new AmountRecipeIngredientsController(_unitOfWorkMock.Object);
            _amountIngredient = new AmountIngredient() { Id = 1, Amount = 22.2, Unit = "g", RecipeId = 1, IngredientId = 2 };
        }

        [Fact]
        public async Task AddAmountIngredient_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(_amountIngredient));

            // Act
            await _amountRecipeIngredientsController.AddAsync(_amountIngredient.Amount,
                _amountIngredient.Unit, _amountIngredient.RecipeId, _amountIngredient.IngredientId);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteAmountIngredient_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(_amountIngredient));

            // Act
            await _amountRecipeIngredientsController.DeleteAsync(1);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAmountIngrediets_Should_Count()
        {
            // Arrange
            List<AmountIngredient> amountIngredients = new List<AmountIngredient>
            {
                new AmountIngredient()
                {
                    Id = 1,
                    Amount = 3.3,
                    Unit = "g",
                    IngredientId = 12,
                    RecipeId = 3
                },
                new AmountIngredient()
                {
                    Id = 2,
                    Amount = 4.4,
                    Unit = "tbs",
                    IngredientId = 2,
                    RecipeId = 2
                },
                new AmountIngredient()
                {
                    Id = 3,
                    Amount = 5.4,
                    Unit = "tbs",
                    IngredientId = 13,
                    RecipeId = 2
                },
            };

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(amountIngredients.Where(x => x.RecipeId == 2).ToList());

            // Act
            int amountIngredientsCount = (await _amountRecipeIngredientsController.GetAmountIngredietsRecipeAsync(2)).Count;

            // Assert
            Assert.Equal(2, amountIngredientsCount);
        }

        [Fact]
        public async Task GetAmountIngredientName_Should_IngredientName()
        {
            // Arrange
            Ingredient ingredient = new Ingredient() { Id = 1, Name = "Banana" };
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(ingredient);

            // Act
            string ingredientName = (await _amountRecipeIngredientsController.GetAmountIngredientNameAsync(1));

            // Assert
            Assert.Equal("Banana", ingredientName);
        }
    }
}
