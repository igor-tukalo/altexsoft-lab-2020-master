using HomeTask4.Core;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace HomeTask5.Core.Tests
{
    public class IngredientsControllerTests : BaseTests
    {
        private readonly Mock<IOptions<CustomSettings>> _optionsMock;
        private readonly IngredientsController _ingredientsController;
        private readonly List<Ingredient> _ingredients;
        private readonly Ingredient _ingredient;
        private readonly CustomSettings _customSettings;

        public IngredientsControllerTests()
        {
            _customSettings = new CustomSettings() { NumberConsoleLines = 2 };
            _optionsMock = new Mock<IOptions<CustomSettings>>();
            _optionsMock.Setup(ap => ap.Value).Returns(_customSettings);

            _ingredientsController = new IngredientsController(_unitOfWorkMock.Object, _optionsMock.Object);

            _ingredients = new List<Ingredient>()
            {
                new Ingredient()
                {
                    Id = 1,
                    Name= "Banana"
                },
                new Ingredient()
                {
                    Id = 2,
                    Name= "Milk"
                },
                new Ingredient()
                {
                    Id = 3,
                    Name= "Kefir"
                }
            };

            _ingredient = _ingredients.First(x => x.Id == 1);
        }

        [Fact]
        public async Task GetIngredientByIdAsync_Should_ReturnIngredientById()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(_ingredient.Id))
                .ReturnsAsync(_ingredient).Verifiable();

            // Act
            Ingredient ingredientActual = await _ingredientsController.GetIngredientByIdAsync(_ingredient.Id);

            //Assert
            Assert.Same(_ingredient, ingredientActual);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetIngredientsBatch_Should_ReturnNumberBatches()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(_ingredients).Verifiable();

            // Act
            int numberBatches = (await _ingredientsController.GetIngredientsBatchAsync()).Count;

            //Assert
            Assert.Equal(_customSettings.NumberConsoleLines, numberBatches);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task AddAsync_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.Is<Ingredient>(
                entity => entity.Name == _ingredient.Name))).Verifiable();

            // Act
            await _ingredientsController.AddAsync(_ingredient.Name);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAllIngredients_Should_ReturnNumberIngredients()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(_ingredients).Verifiable();

            // Act
            int categoriesCount = (await _ingredientsController.GetAllIngredients()).Count;

            // Assert
            Assert.Equal(_ingredients.Count, categoriesCount);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task FindIngredients_Should_ReturnNumberIngredientsFound()
        {
            // Arrange
            string sarchValue = "a";
            List<Ingredient> ingredintsSearch = _ingredients.Where(x => x.Name.ToLower().Contains(sarchValue)).ToList();
            int expectedValue = ingredintsSearch.Count;

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Ingredient, bool>>>()))
                .ReturnsAsync(ingredintsSearch).Verifiable();

            // Act
            int ingredientsCount = (await _ingredientsController.FindIngredientsAsync(sarchValue)).Count;

            // Assert
            Assert.Equal(expectedValue, ingredientsCount);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task RenameAsync_Should_RenameExistingIngredient()
        {
            // Arrange
            string newName = "Apple";
            _repositoryMock.Setup(o => o.UpdateAsync(It.Is<Ingredient>(
                entity => entity.Id == _ingredient.Id &&
                entity.Name == _ingredient.Name)));

            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(_ingredient.Id))
                .ReturnsAsync(_ingredient);

            // Act
            await _ingredientsController.RenameAsync(_ingredient.Id, newName);

            // Assert
            string updatedName = (await _ingredientsController.GetIngredientByIdAsync(_ingredient.Id)).Name;
            Assert.Equal(newName, updatedName);
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteIngredient_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(It.Is<Ingredient>(
                entity => entity.Id == _ingredient.Id &&
                entity.Name == _ingredient.Name)));

            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(_ingredient.Id))
                .ReturnsAsync(_ingredient);

            // Act
            await _ingredientsController.DeleteAsync(_ingredient.Id);

            // Assert
            Ingredient ingredientToDelete = await _ingredientsController.GetIngredientByIdAsync(_ingredient.Id);
            Assert.Same(_ingredient, ingredientToDelete);
            _repositoryMock.VerifyAll();
        }
    }
}
