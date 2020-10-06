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
    public class AmountRecipeIngredientsControllerTests
    {
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private AmountRecipeIngredientsController controller;
        public AmountRecipeIngredientsControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);
        }
        [Fact]
        public async Task AddAmountIngredient_Should_True()
        {
            // Arrange
            bool isAdded = false;
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<AmountIngredient>())).Callback(() => isAdded = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.AddAsync(It.IsAny<double>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task DeleteAmountIngredient_Should_True()
        {
            // Arrange
            bool isDelete = false;
            repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<AmountIngredient>())).Callback(() => isDelete = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.DeleteAsync(It.IsAny<int>());
            // Assert
            Assert.True(isDelete);
        }

        [Fact]
        public async Task GetAmountIngrediets_Should_Count()
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
                    RecipeId = 2
                },
                new AmountIngredient()
                {
                    Id = It.IsAny<int>(),
                    Amount = It.IsAny<double>(),
                    Unit = It.IsAny<string>(),
                    IngredientId = It.IsAny<int>(),
                    RecipeId = 2
                },
            };

            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(amountIngredients.Where(x => x.RecipeId == 2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int amountIngredientsCount = (await controller.GetAmountIngredietsAsync(2)).Count;
            // Assert
            Assert.Equal(2, amountIngredientsCount);
        }

        [Fact]
        public async Task GetAmountIngredientName_Should_IngredientName()
        {
            // Arrange
            Ingredient ingredient = new Ingredient() { Id = 1, Name = "Banana" };
            repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(It.IsAny<int>()))
                .ReturnsAsync(ingredient);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);
            // Act
            controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            string ingredientName = (await controller.GetAmountIngredientNameAsync(It.IsAny<int>()));
            // Assert
            Assert.Equal("Banana", ingredientName);
        }
    }
}
