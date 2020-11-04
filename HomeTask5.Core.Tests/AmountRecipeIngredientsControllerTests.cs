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
        public async Task AddAsync_Should_ReturnVerified()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.IsAny<AmountIngredient>())).Verifiable();

            // Act
            await _amountRecipeIngredientsController.AddAsync(_amountIngredient.Amount,
                _amountIngredient.Unit, _amountIngredient.RecipeId, _amountIngredient.IngredientId);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteAsync_Should_ReturnVerified()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(_amountIngredient)).Verifiable();
            _repositoryMock.Setup(o => o.GetByIdAsync<AmountIngredient>(_amountIngredient.Id)).ReturnsAsync(_amountIngredient).Verifiable();

            // Act
            await _amountRecipeIngredientsController.DeleteAsync(_amountIngredient.Id);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAmountIngredietsRecipeAsync_Should_ReturnNumberAmountngredientsRecipe()
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

            List<AmountIngredient> amountIngredientsWhereRecipeId = amountIngredients.Where(x => x.RecipeId == _amountIngredient.RecipeId).ToList();
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(amountIngredientsWhereRecipeId).Verifiable();

            // Act
            int amountIngredientsCount = (await _amountRecipeIngredientsController.GetAmountIngredietsRecipeAsync(_amountIngredient.RecipeId)).Count;

            // Assert
            Assert.Equal(amountIngredientsWhereRecipeId.Count(), amountIngredientsCount);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAmountIngredientNameAsync_Should_ReturnIngredientName()
        {
            // Arrange
            Ingredient ingredient = new Ingredient() { Id = 1, Name = "Banana" };
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(ingredient.Id))
                .ReturnsAsync(ingredient).Verifiable();

            // Act
            string ingredientName = await _amountRecipeIngredientsController.GetAmountIngredientNameAsync(ingredient.Id);

            // Assert
            Assert.Equal(ingredient.Name, ingredientName);
            _repositoryMock.Verify();
        }
    }
}
