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
    public class CookingStepsControllerTests
    {
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private CookingStepsController controller;
        private List<CookingStep> cookingSteps;
        public CookingStepsControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);

            cookingSteps = new List<CookingStep>
            {
                new CookingStep()
                {
                    Id = 1,
                    Step=1,
                    Name = It.IsAny<string>(),
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 2,
                    Step=2,
                    Name = It.IsAny<string>(),
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 3,
                    Step=3,
                    Name = It.IsAny<string>(),
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 4,
                    Step=1,
                    Name = It.IsAny<string>(),
                    RecipeId = 2
                },
            };
        }
        [Fact]
        public async Task GetCookingStepsWhereRecipeId_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
               .ReturnsAsync(cookingSteps.Where(x => x.RecipeId == 2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CookingStepsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int cookingStepsCount = (await controller.GetCookingStepsWhereRecipeIdAsync(2)).Count;
            // Assert
            Assert.Equal(1, cookingStepsCount);
        }

        [Fact]
        public async Task GetCookingStepById_Should_CookingStepName()
        {
            // Arrange
            CookingStep cookingStep = new CookingStep()
            {
                Id = It.IsAny<int>(),
                Name = "Test",
                RecipeId = It.IsAny<int>()
            };

            repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(It.IsAny<int>()))
                .ReturnsAsync(cookingStep);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CookingStepsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            string cookingStepName = (await controller.GetCookingStepByIdAsync(It.IsAny<int>())).Name;

            //Assert
            Assert.Equal("Test", cookingStepName);
        }

        [Fact]
        public async Task AddCookingStep_Should_True()
        {
            // Arrange
            bool isAdded = false;
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<CookingStep>())).Callback(() => isAdded = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CookingStepsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.AddAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task EditCookingStep_Should_NewName()
        {
            // Arrange
            bool isUpdate = false;
            CookingStep cookingStep = new CookingStep() { Id = 1, Step = 1, Name = "Test", RecipeId = 1 };
            repositoryMock.Setup(o => o.UpdateAsync(It.IsAny<CookingStep>())).Callback(() => isUpdate = true);
            repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(It.IsAny<int>()))
                .ReturnsAsync(cookingStep);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CookingStepsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            cookingStep.Name = "Peel potatoes";
            await controller.EditAsync(cookingStep);
            string updatedName = (await controller.GetCookingStepByIdAsync(1)).Name;
            // Assert
            Assert.Equal("Peel potatoes", updatedName);
            Assert.True(isUpdate);
        }

        [Fact]
        public async Task DeleteCookingStep_Should_True()
        {
            // Arrange
            bool isDelete = false;
            repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<CookingStep>())).Callback(() => isDelete = true);
            repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(It.IsAny<int>()))
                .ReturnsAsync(cookingSteps.Find(x => x.Id == 1));

            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
                .ReturnsAsync(cookingSteps.Where(x => x.RecipeId == cookingSteps.Find(x => x.Id == 1).RecipeId).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CookingStepsController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            await controller.DeleteAsync(1);
            int numCookingStep = (await controller.GetCookingStepsWhereRecipeIdAsync(1)).Max(x => x.Step);
            // Assert
            Assert.True(isDelete);
            Assert.Equal(2, numCookingStep);
        }
    }
}
