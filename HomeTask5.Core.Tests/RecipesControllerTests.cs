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
        private readonly List<Recipe> recipes;
        private readonly Recipe _recipe;
        public RecipesControllerTests()
        {
            _recipesController = new RecipesController(_unitOfWorkMock.Object);

            _recipe = new Recipe()
            {
                Id = 1,
                Name = "Omelette"
            };

            recipes = new List<Recipe>()
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
        }

        [Fact]
        public async Task GetRecipeById_Shjould_NameRecipe()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1)).ReturnsAsync(_recipe);

            // Act
            string recipeName = (await _recipesController.GetRecipeByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Omelette", recipeName);
        }

        [Fact]
        public async Task GetRecipesWhereCategoryId_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Recipe, bool>>>()))
               .ReturnsAsync(recipes.Where(x => x.CategoryId == 2).ToList());

            // Act
            int recipesCount = (await _recipesController.GetRecipesWhereCategoryIdAsync(2)).Count;

            // Assert
            Assert.Equal(2, recipesCount);
        }

        [Fact]
        public async Task AddRecipe_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(It.IsAny<Recipe>()));

            // Act
            await _recipesController.AddAsync(_recipe.Name, _recipe.Description, _recipe.CategoryId);

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task RenameRecipe_Should_NewName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.UpdateAsync(_recipe));
            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.RenameAsync(1, "Shakshuka");
            string updatedName = (await _recipesController.GetRecipeByIdAsync(1)).Name;

            // Assert
            Assert.Equal("Shakshuka", updatedName);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task ChangeDescription_Should_NewDescription()
        {
            // Arrange
            _repositoryMock.Setup(o => o.UpdateAsync(_recipe));
            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.ChangeDescriptionAsync(1, "Vary this popular brunch");
            string updatedDesc = (await _recipesController.GetRecipeByIdAsync(1)).Description;

            // Assert
            Assert.Equal("Vary this popular brunch", updatedDesc);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteRecipe_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.DeleteAsync(_recipe));
            _repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(_recipe);

            // Act
            await _recipesController.DeleteAsync(1);

            // Assert
            _repositoryMock.Verify();
        }
    }
}
