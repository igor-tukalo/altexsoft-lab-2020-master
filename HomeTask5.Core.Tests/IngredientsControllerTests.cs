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

        public IngredientsControllerTests()
        {
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 2 };
            _optionsMock = new Mock<IOptions<CustomSettings>>();
            _optionsMock.Setup(ap => ap.Value).Returns(app);

            _ingredientsController = new IngredientsController(_unitOfWorkMock.Object, _optionsMock.Object);

            _ingredient = new Ingredient()
            {
                Id = 1,
                Name = "Banana"
            };

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
        }

        [Fact]
        public async Task GetIngredientById_Should_IngredientName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(_ingredient);

            // Act
            string ingredientName = (await _ingredientsController.GetIngredientByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Banana", ingredientName);
        }

        [Fact]
        public async Task GetIngredientsBatch_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(_ingredients);

            // Act
            int ingredientsCount = (await _ingredientsController.GetIngredientsBatchAsync()).Count;

            //Assert
            Assert.Equal(2, ingredientsCount);
        }

        [Fact]
        public async Task AddIngredient_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(_ingredient));

            // Act
            await _ingredientsController.AddAsync("Tomato");

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetAllIngredients_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(_ingredients);

            // Act
            int categoriesCount = (await _ingredientsController.GetAllIngredients()).Count;

            // Assert
            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public async Task FindIngredients_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Ingredient, bool>>>()))
                .ReturnsAsync(_ingredients.Where(x => x.Name.ToLower().Contains("a")).ToList());

            // Act
            int ingredientsCount = (await _ingredientsController.FindIngredientsAsync("a")).Count;

            // Assert
            Assert.Equal(1, ingredientsCount);
        }

        [Fact]
        public async Task RenameIngredient_Should_NewName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.UpdateAsync(_ingredient));
            _repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(_ingredient);

            // Act
            await _ingredientsController.RenameAsync(1, "Apple");
            string updatedName = (await _ingredientsController.GetIngredientByIdAsync(1)).Name;

            // Assert
            Assert.Equal("Apple", updatedName);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteIngredient_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<Ingredient>()));

            // Act
            await _ingredientsController.DeleteAsync(It.IsAny<int>());

            // Assert
            _repositoryMock.Verify();
        }
    }
}
