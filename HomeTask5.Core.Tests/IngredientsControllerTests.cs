using HomeTask4.Core;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
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
    public class IngredientsControllerTests
    {
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IngredientsController controller;
        private List<Ingredient> ingredients;

        public IngredientsControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 2 };
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);
            ingredients = new List<Ingredient>()
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
            Ingredient ingredient = new Ingredient()
            {
                Id = 1,
                Name = "Banana"
            };

            repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(ingredient);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            string ingredientName = (await controller.GetIngredientByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Banana", ingredientName);
        }

        [Fact]
        public async Task GetIngredientsBatch_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(ingredients);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int ingredientsCount = (await controller.GetIngredientsBatchAsync()).Count;

            //Assert
            Assert.Equal(2, ingredientsCount);
        }

        [Fact]
        public async Task AddIngredient_Should_True()
        {
            // Arrange
            bool isAdded = false;
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<Ingredient>())).Callback(() => isAdded = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.AddAsync(It.IsAny<string>());
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task GetIngredientsWhereRecipeId_Should_Count()
        {
            // Arrange
            List<AmountIngredient> amountIngredients = new List<AmountIngredient>
            {
                new AmountIngredient()
                {
                    Id = It.IsAny<int>(),
                    Amount = It.IsAny<double>(),
                    Unit = It.IsAny<string>(),
                    IngredientId = It.IsAny<int>(),
                    RecipeId = It.IsAny<int>()
                },
                new AmountIngredient()
                {
                    Id = It.IsAny<int>(),
                    Amount = It.IsAny<double>(),
                    Unit = It.IsAny<string>(),
                    IngredientId = It.IsAny<int>(),
                    RecipeId = It.IsAny<int>()
                },
                new AmountIngredient()
                {
                    Id = It.IsAny<int>(),
                    Amount = It.IsAny<double>(),
                    Unit = It.IsAny<string>(),
                    IngredientId = 1,
                    RecipeId = 2
                },
            };

            Ingredient ingredient = new Ingredient()
            {
                Id = 1,
                Name = "Banana"
            };

            repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(ingredient);

            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(amountIngredients.Where(x => x.RecipeId == 2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int amountIngredientsCount = (await controller.GetIngredientsWhereRecipeIdAsync(2)).Count;
            // Assert
            Assert.Equal(1, amountIngredientsCount);
        }

        [Fact]
        public async Task GetAllIngredients_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListAsync<Ingredient>())
                .ReturnsAsync(ingredients);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int categoriesCount = (await controller.GetAllIngredients()).Count;
            // Assert
            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public async Task FindIngredients_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Ingredient, bool>>>()))
                .ReturnsAsync(ingredients.Where(x => x.Name.ToLower().Contains("a")).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int ingredientsCount = (await controller.FindIngredientsAsync("a")).Count;
            // Assert
            Assert.Equal(1, ingredientsCount);
        }

        [Fact]
        public async Task RenameIngredient_Should_NewName()
        {
            // Arrange
            bool isUpdate = false;
            Ingredient ingredient = new Ingredient() { Id = 1, Name = "Banana" };
            repositoryMock.Setup(o => o.UpdateAsync(It.IsAny<Ingredient>())).Callback(() => isUpdate = true);
            repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(1))
                .ReturnsAsync(ingredient);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.RenameAsync(1, "Apple");
            string updatedName = (await controller.GetIngredientByIdAsync(1)).Name;
            // Assert
            Assert.Equal("Apple", updatedName);
            Assert.True(isUpdate);
        }

        [Fact]
        public async Task DeleteIngredient_Should_True()
        {
            // Arrange
            bool isDelete = false;
            repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<Ingredient>())).Callback(() => isDelete = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new IngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.DeleteAsync(It.IsAny<int>());
            // Assert
            Assert.True(isDelete);
        }
    }
}
