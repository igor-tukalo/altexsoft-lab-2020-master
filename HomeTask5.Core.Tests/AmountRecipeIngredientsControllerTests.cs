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
            _amountIngredient = new AmountIngredient()
            {
                Id = 1,
                Amount = 22.2,
                Unit = "g",
                RecipeId = 1,
                IngredientId = 2,
                Ingredient = new Ingredient() { Id = 2, Name = "Banana" }
            };
        }

        [Fact]
        public async Task AddAsync_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.Is<AmountIngredient>(
                entity => entity.Amount == _amountIngredient.Amount
                && entity.IngredientId == _amountIngredient.IngredientId
                && entity.RecipeId == _amountIngredient.RecipeId
                && entity.Unit == _amountIngredient.Unit))).Verifiable();

            // Act
            await _amountRecipeIngredientsController.AddAsync(_amountIngredient.Amount,
                _amountIngredient.Unit, _amountIngredient.RecipeId, _amountIngredient.IngredientId);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteAsync_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(It.Is<AmountIngredient>(
                entity => entity.Id == _amountIngredient.Id &&
                entity.Amount == _amountIngredient.Amount &&
                entity.Unit == _amountIngredient.Unit &&
                entity.IngredientId == _amountIngredient.IngredientId &&
                entity.RecipeId == _amountIngredient.RecipeId))).Verifiable();

            _repositoryMock.Setup(o => o.GetByIdAsync<AmountIngredient>(_amountIngredient.Id)).ReturnsAsync(_amountIngredient).Verifiable();

            // Act
            await _amountRecipeIngredientsController.DeleteAsync(_amountIngredient.Id);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAmountIngredietsRecipeAsync_Should_ReturnAmountngredientsRecipe()
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

            List<AmountIngredient> expectedResult = amountIngredients.Where(x => x.RecipeId == _amountIngredient.RecipeId).ToList();

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(expectedResult).Verifiable();

            // Act
            List<AmountIngredient> result = await _amountRecipeIngredientsController.GetAmountIngredietsRecipeAsync(_amountIngredient.RecipeId);

            // Assert
            Assert.Same(expectedResult, result);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAmountIngredientNameAsync_Should_ReturnAmountIngredientName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(_amountIngredient.IngredientId))
                .ReturnsAsync(_amountIngredient.Ingredient).Verifiable();

            // Act
            string ingredientName = await _amountRecipeIngredientsController.GetAmountIngredientNameAsync(_amountIngredient.IngredientId);

            // Assert
            Assert.Equal(_amountIngredient.Ingredient.Name, ingredientName);
            _repositoryMock.Verify();
        }
    }
}
