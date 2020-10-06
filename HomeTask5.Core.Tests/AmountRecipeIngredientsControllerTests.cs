using HomeTask4.Core;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace HomeTask5.Core.Tests
{
    public class AmountRecipeIngredientsControllerTests
    {
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        public AmountRecipeIngredientsControllerTests()
        {
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };                                                                       // Make sure you include using Moq;
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);
        }
        [Fact]
        public async Task AddAmountIngredient_Should_True()
        {
            bool isAdded = false;

            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<AmountIngredient>())).Callback(() => isAdded = true);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            AmountRecipeIngredientsController controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            await controller.AddAsync(It.IsAny<double>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task DeleteAmountIngredient_Should_True()
        {
            bool isDelete = false;
            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<AmountIngredient>())).Callback(() => isDelete = true);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            AmountRecipeIngredientsController controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            await controller.DeleteAsync(It.IsAny<int>());
            // Assert
            Assert.True(isDelete);
        }

        [Fact]
        public async Task GetAmountIngredietsAsync_Should_Count()
        {
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
                    IngredientId = It.IsAny<int>(),
                    RecipeId = It.IsAny<int>()
                },
            };

            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<AmountIngredient, bool>>>()))
                .ReturnsAsync(new  List<AmountIngredient>(amountIngredients));

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            AmountRecipeIngredientsController controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            int amountIngredientsCount = (await controller.GetAmountIngredietsAsync(It.IsAny<int>())).Count;

            Assert.Equal(3, amountIngredientsCount);
        }

        [Fact]
        public async Task GetAmountIngredientNameAsync_Should_IngredientName()
        {
            var ingredient = new Ingredient() { Id = 1, Name = "Banana" };
            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(o => o.GetByIdAsync<Ingredient>(It.IsAny<int>()))
                .ReturnsAsync(ingredient);

            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            AmountRecipeIngredientsController controller = new AmountRecipeIngredientsController(unitOfWorkMock.Object, optionsMock.Object);
            string ingredientName = (await controller.GetAmountIngredientNameAsync(It.IsAny<int>()));
            Assert.Equal("Banana", ingredientName);
        }
    }
}
