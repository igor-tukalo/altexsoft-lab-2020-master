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
    public class RecipesControllerTests : BaseTests
    {
        private readonly RecipesController _recipesController;
        private readonly List<Recipe> _recipes;
        private readonly Recipe _recipe;
        public RecipesControllerTests()
        {
            _recipesController = new RecipesController(_unitOfWorkMock.Object);

            _recipes = new List<Recipe>()
            {
                new Recipe()
                {
                    Id=1,
                    Name= "American pancakes",
                    Description= "test1",
                    CategoryId=2
                },
                new Recipe()
                {
                    Id=2,
                    Name= "Eggy cheese crumpets",
                    Description= "test2",
                    CategoryId=2
                },
                new Recipe()
                {
                    Id=3,
                    Name= "Treacle tart",
                    Description= "test3",
                    CategoryId=4
                },
                new Recipe()
                {
                    Id=4,
                    Name= "Damson crumble",
                    Description= "test4",
                    CategoryId=4
                }
            };

            _recipe = _recipes.Find(x => x.Id == 1);
        }

        [Fact]
        public async Task GetRecipeByIdAsync_Should_ReturnRequestedRecipe()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(_recipe.Id)).ReturnsAsync(_recipe).Verifiable();

            // Act
            Recipe recipeResult = await _recipesController.GetRecipeByIdAsync(_recipe.Id);

            //Assert
            Assert.Same(_recipe, recipeResult);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task GetRecipesWhereCategoryIdAsync_Should_ReturnRecipes()
        {
            // Arrange
            List<Recipe> expectedResult = _recipes.Where(x => x.CategoryId == _recipe.CategoryId).ToList();

            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Recipe, bool>>>()))
               .ReturnsAsync(expectedResult).Verifiable();

            // Act
            List<Recipe> result = await _recipesController.GetRecipesWhereCategoryIdAsync(_recipe.CategoryId);

            // Assert
            Assert.Same(expectedResult, result);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task AddAsync_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.Is<Recipe>(
                entity => entity.Name == _recipe.Name &&
                entity.Description == _recipe.Description &&
                entity.CategoryId == _recipe.CategoryId))).Verifiable();

            // Act
            await _recipesController.AddAsync(_recipe.Name, _recipe.Description, _recipe.CategoryId);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task RenameAsync_Should_RenameExistingRecipe()
        {
            // Arrange
            string newNameRecipe = "Shakshuka";

            _repositoryMock.Setup(o => o.UpdateAsync(It.Is<Recipe>(
                entity => entity.Name == _recipe.Name &&
                entity.Description == _recipe.Description &&
                entity.CategoryId == _recipe.CategoryId)));

            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(_recipe.Id))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.RenameAsync(_recipe.Id, newNameRecipe);

            // Assert
            string updatedName = (await _recipesController.GetRecipeByIdAsync(_recipe.Id)).Name;
            Assert.Equal(newNameRecipe, updatedName);
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task ChangeDescription_Should_EditExistingRecipeDescription()
        {
            // Arrange
            string newDesc = "Vary this popular brunch";

            _repositoryMock.Setup(o => o.UpdateAsync(It.Is<Recipe>(
                entity => entity.Name == _recipe.Name &&
                entity.Description == _recipe.Description &&
                entity.CategoryId == _recipe.CategoryId)));

            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(_recipe.Id))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.ChangeDescriptionAsync(_recipe.Id, newDesc);

            // Assert
            string updatedDesc = (await _recipesController.GetRecipeByIdAsync(_recipe.Id)).Description;
            Assert.Equal(newDesc, updatedDesc);
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task DeleteRecipe_Runs_Properly()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(It.Is<Recipe>(
                entity => entity.Name == _recipe.Name &&
                entity.Description == _recipe.Description &&
                entity.CategoryId == _recipe.CategoryId)));

            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(_recipe.Id))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.DeleteAsync(_recipe.Id);

            // Assert
            Recipe recipeToDelete = await _recipesController.GetRecipeByIdAsync(_recipe.Id);
            Assert.Same(_recipe, recipeToDelete);
            _repositoryMock.VerifyAll();
        }
    }
}
