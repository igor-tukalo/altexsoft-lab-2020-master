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

            _cookingStep = _cookingSteps.First(x => x.Id == 1);
        }

        [Fact]
        public async Task GetCookingStepsWhereRecipeIdAsync_Should_ReturnNumberCookingStepsWhereRecipeId()
        {
            // Arrange
            List<CookingStep> cookingStepWhereRecipeId = _cookingSteps.Where(x => x.RecipeId == _cookingStep.RecipeId).ToList();
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
               .ReturnsAsync(cookingStepWhereRecipeId).Verifiable();

            // Act
            int cookingStepsCount = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(_cookingStep.RecipeId)).Count;

            // Assert
            Assert.Equal(cookingStepWhereRecipeId.Count(), cookingStepsCount);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetCookingStepById_Should_ReturnCookingStepById()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(_cookingStep.Id))
                .ReturnsAsync(_cookingStep).Verifiable();

            // Act
            CookingStep cookingStepActual = await _cookingStepsController.GetCookingStepByIdAsync(_cookingStep.Id);

            //Assert
            Assert.Equal(_cookingStep, cookingStepActual);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task AddAsync_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.Is<CookingStep>(
                entity => entity.Name == _cookingStep.Name &&
                entity.Step == _cookingStep.Step &&
                entity.RecipeId == _cookingStep.RecipeId))).Verifiable();

            // Act
            await _cookingStepsController.AddAsync(_cookingStep.RecipeId, _cookingStep.Step, _cookingStep.Name);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task EditAsync_Should_EditExistingCookingStep()
        {
            // Arrange
            string newName = "Peel potatoes";
            int newStep = 2;
            _repositoryMock.Setup(o => o.UpdateAsync(_cookingStep));
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(_cookingStep.Id))
                .ReturnsAsync(_cookingStep);
            _cookingStep.Name = newName;
            _cookingStep.Step = newStep;

            // Act
            await _cookingStepsController.EditAsync(_cookingStep);

            // Assert
            CookingStep updatedCookingStep = await _cookingStepsController.GetCookingStepByIdAsync(_cookingStep.Id);
            Assert.Equal(newName, updatedCookingStep.Name);
            Assert.Equal(newStep, updatedCookingStep.Step);
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteAsync_Runs_Properly()
        {
            // Arrange
            List<CookingStep> cookingStepsWhereRecipeId =
                _cookingSteps.Where(x => x.RecipeId == _cookingSteps.Find(x => x.Id == _cookingStep.Id).RecipeId).ToList();
            _repositoryMock.Setup(o => o.DeleteAsync(_cookingStep));
            _repositoryMock.Setup(o => o.GetByIdAsync<CookingStep>(_cookingStep.Id))
                .ReturnsAsync(_cookingStep);

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<CookingStep, bool>>>()))
                .ReturnsAsync(cookingStepsWhereRecipeId);

            // Act
            await _cookingStepsController.DeleteAsync(_cookingStep.Id);

            // Assert
            int numCookingStep = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(_cookingStep.Id)).Max(x => x.Step);
            Assert.Equal(cookingStepsWhereRecipeId.Max(x => x.Step), numCookingStep);
            _repositoryMock.VerifyAll();
        }
    }
}
