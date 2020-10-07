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

        public IngredientsControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 2 };
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);
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
            List<Ingredient> ingredients = new List<Ingredient>()
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
    }
}
