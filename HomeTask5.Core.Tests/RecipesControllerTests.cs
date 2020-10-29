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
    public class RecipesControllerTests
    {
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private RecipesController controller;
        private readonly List<Category> categories;
        private readonly List<Recipe> recipes;
        public RecipesControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);

            categories = new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name= "Categories",
                    ParentId=0
                },
                new Category()
                {
                    Id=2,
                    Name= "Breakfasts",
                    ParentId=1
                },
                new Category()
                {
                    Id=3,
                    Name= "Dinners",
                    ParentId=1
                },
                new Category()
                {
                    Id=4,
                    Name= "desserts",
                    ParentId=1
                }
            };

            recipes = new List<Recipe>()
            {
                new Recipe()
                {
                    Id=1,
                    Name= "American pancakes",
                    Description= It.IsAny<string>(),
                    CategoryId=2
                },
                new Recipe()
                {
                    Id=2,
                    Name= "Eggy cheese crumpets",
                    Description= It.IsAny<string>(),
                    CategoryId=2
                },
                new Recipe()
                {
                    Id=3,
                    Name= "Treacle tart",
                    Description= It.IsAny<string>(),
                    CategoryId=4
                },
                new Recipe()
                {
                    Id=4,
                    Name= "Damson crumble",
                    Description= It.IsAny<string>(),
                    CategoryId=4
                }
            };
        }

        [Fact]
        public async Task GetRecipeById_Shjould_NameRecipe()
        {
            // Arrange
            Recipe recipe = new Recipe()
            {
                Id = 1,
                Name = "Omelette"
            };

            repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(recipe);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            string recipeName = (await controller.GetRecipeByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Omelette", recipeName);
        }



        [Fact]
        public async Task GetRecipesWhereCategoryId_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Recipe, bool>>>()))
               .ReturnsAsync(recipes.Where(x => x.CategoryId == 2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            int recipesCount = (await controller.GetRecipesWhereCategoryIdAsync(2)).Count;
            // Assert
            Assert.Equal(2, recipesCount);
        }

        [Fact]
        public async Task AddRecipe_Should_True()
        {
            // Arrange
            bool isAdded = false;
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<Recipe>())).Callback(() => isAdded = true);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            await controller.AddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task RenameRecipe_Should_NewName()
        {
            // Arrange
            bool isUpdate = false;
            Recipe recipe = new Recipe() { Id = 1, Name = "Omlette", Description = It.IsAny<string>() };
            repositoryMock.Setup(o => o.UpdateAsync(It.IsAny<Recipe>())).Callback(() => isUpdate = true);
            repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(recipe);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            await controller.RenameAsync(1, "Shakshuka");
            string updatedName = (await controller.GetRecipeByIdAsync(1)).Name;
            // Assert
            Assert.Equal("Shakshuka", updatedName);
            Assert.True(isUpdate);
        }

        [Fact]
        public async Task ChangeDescription_Should_NewDescription()
        {
            // Arrange
            bool isUpdate = false;
            Recipe recipe = new Recipe() { Id = 1, Name = "Omlette", Description = "Test" };
            repositoryMock.Setup(o => o.UpdateAsync(It.IsAny<Recipe>())).Callback(() => isUpdate = true);
            repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(recipe);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            await controller.ChangeDescriptionAsync(1, "Vary this popular brunch");
            string updatedDesc = (await controller.GetRecipeByIdAsync(1)).Description;
            // Assert
            Assert.Equal("Vary this popular brunch", updatedDesc);
            Assert.True(isUpdate);
        }

        [Fact]
        public async Task DeleteRecipe_Should_True()
        {
            // Arrange
            bool isDelete = false;
            Recipe recipe = new Recipe() { Id = 1, Name = "Omlette", Description = "Test" };
            repositoryMock.Setup(o => o.DeleteAsync(recipe)).Callback(() => isDelete = true);
            repositoryMock.Setup(o => o.GetByIdAsync<Recipe>(1))
                .ReturnsAsync(recipe);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new RecipesController(unitOfWorkMock.Object);
            // Act
            await controller.DeleteAsync(1);
            // Assert
            Assert.True(isDelete);
        }
    }
}
