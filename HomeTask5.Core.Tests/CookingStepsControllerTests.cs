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
    public class CookingStepsControllerTests : BaseTests
    {
        private readonly CookingStepsController _cookingStepsController;
        private readonly List<CookingStep> _cookingSteps;
        private readonly CookingStep _cookingStep;
        public CookingStepsControllerTests()
        {
            _cookingStepsController = new CookingStepsController(_unitOfWorkMock.Object);
            _cookingStep = new CookingStep()
            {
                Id = 1,
                Name = "Test",
                Step = 1,
                RecipeId = 1
            };
            _cookingSteps = new List<CookingStep>
            {
                new CookingStep()
                {
                    Id = 1,
                    Step=1,
                    Name ="test",
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 2,
                    Step=2,
                    Name = "test2",
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 3,
                    Step=3,
                    Name = "test3",
                    RecipeId = 1
                },
                new CookingStep()
                {
                    Id = 4,
                    Step=1,
                    Name = "test4",
                    RecipeId = 2
                },
            };
        }

        [Fact]
        public async Task GetCookingStepsWhereRecipeId_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
               .ReturnsAsync(_cookingSteps.Where(x => x.RecipeId == 2).ToList());

            // Act
            int cookingStepsCount = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(2)).Count;

            // Assert
            Assert.Equal(1, cookingStepsCount);
        }

        [Fact]
        public async Task GetCookingStepById_Should_CookingStepName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(1))
                .ReturnsAsync(_cookingStep);

            // Act
            string cookingStepName = (await _cookingStepsController.GetCookingStepByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Test", cookingStepName);
        }

        [Fact]
        public async Task AddCookingStep_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(_cookingStep));

            // Act
            await _cookingStepsController.AddAsync(_cookingStep.RecipeId, _cookingStep.Step, _cookingStep.Name);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task EditCookingStep_Should_NewName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.UpdateAsync(_cookingStep));
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(1))
                .ReturnsAsync(_cookingStep);

            // Act
            _cookingStep.Name = "Peel potatoes";
            await _cookingStepsController.EditAsync(_cookingStep);
            string updatedName = (await _cookingStepsController.GetCookingStepByIdAsync(1)).Name;

            // Assert
            Assert.Equal("Peel potatoes", updatedName);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteCookingStep_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(_cookingStep));
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(1))
                .ReturnsAsync(_cookingSteps.Find(x => x.Id == 1));

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
                .ReturnsAsync(_cookingSteps.Where(x => x.RecipeId == _cookingSteps.Find(x => x.Id == 1).RecipeId).ToList());

            // Act
            await _cookingStepsController.DeleteAsync(1);
            int numCookingStep = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(1)).Max(x => x.Step);

            // Assert
            _repositoryMock.Verify();
            Assert.Equal(2, numCookingStep);
        }
    }
}
