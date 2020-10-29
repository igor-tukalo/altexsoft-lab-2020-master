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
    public class CategoriesControllerTests
    {
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private CategoriesController controller;
        private List<Category> categories;
        public CategoriesControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };
            categories = new List<Category>
            {
                new Category()
                {
                    Id = 2,
                    Name = "Categories",
                    ParentId = 2
                },
                new Category()
                {
                    Id = 3,
                    Name = "Bakery products",
                    ParentId = 2
                },
                new Category()
                {
                    Id = 4,
                    Name = "Fish, seafood",
                    ParentId = 2
                },
                new Category()
                {
                    Id = 5,
                    Name = "Meat, poultry",
                    ParentId = 2
                },
            };
        }

        [Fact]
        public async Task GetCategoryById_Should_CategoryName()
        {
            // Arrange
            Category category = new Category()
            {
                Id = It.IsAny<int>(),
                Name = "Bakery products",
                ParentId = It.IsAny<int>()
            };

            repositoryMock.Setup(o => o.GetByIdAsync<Category>(It.IsAny<int>()))
                .ReturnsAsync(category);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            string categoryName = (await controller.GetCategoryByIdAsync(It.IsAny<int>())).Name;

            //Assert
            Assert.Equal("Bakery products", categoryName);
        }

        [Fact]
        public async Task GetItemsWhereParentId_Should_Count()
        {
            // Arrange

            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categories.Where(x => x.ParentId == 2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            int categoriesCount = (await controller.GetCategoriesWhereParentIdAsync(2)).Count;
            // Assert
            Assert.Equal(4, categoriesCount);
        }

        [Fact]
        public async Task AddCategory_Should_True()
        {
            // Arrange
            bool isAdded = false;
            repositoryMock.Setup(o => o.AddAsync(It.IsAny<Category>())).Callback(() => isAdded = true);
            repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(new Category() { Id = 1, Name = "Bakery products", ParentId = 1 });

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            await controller.AddAsync(It.IsAny<string>(), "Bakery products");
            // Assert
            Assert.True(isAdded);
        }

        [Fact]
        public async Task RenameCategory_Should_NewName()
        {
            // Arrange
            bool isUpdate = false;
            Category category = new Category() { Id = 1, Name = "Bakery products", ParentId = 1 };
            repositoryMock.Setup(o => o.UpdateAsync(It.IsAny<Category>())).Callback(() => isUpdate = true);
            repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(category);
            repositoryMock.Setup(o => o.GetByIdAsync<Category>(It.IsAny<int>()))
                .ReturnsAsync(category);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            await controller.EditCategoryAsync(1, "Fish",1);
            string updatedName = (await controller.GetCategoryByIdAsync(1)).Name;
            // Assert
            Assert.Equal("Fish", updatedName);
            Assert.True(isUpdate);
        }

        [Fact]
        public async Task Delete_Should_True()
        {
            bool isDelete = false;
            Category category = new Category()
            {
                Id = 2,
                Name = "Categories",
                ParentId = 1
            };
            List<Category> categories = new List<Category>
            {
                new Category()
                {
                    Id = 2,
                    Name = "Categories",
                    ParentId = 2
                },
                                new Category()
                {
                    Id = 3,
                    Name = "Categories2",
                    ParentId = 3
                }
            };
            repositoryMock.Setup(o => o.DeleteAsync(category)).Callback(() => isDelete = true);
            repositoryMock.Setup(o => o.GetByIdAsync<Category>(2))
                .ReturnsAsync(category);
            repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(category);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            await controller.DeleteCategoryAsync(2);
            Assert.True(isDelete);
        }

        [Fact]
        public async Task FindCategories_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categories.Where(x => x.Name.ToLower().Contains("a")).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            int categoriesCount = (await controller.FindCategoriesAsync("a")).Count;
            // Assert
            Assert.Equal(4, categoriesCount);
        }

        [Fact]
        public async Task GetAllGategories_Should_Count()
        {
            // Arrange
            repositoryMock.Setup(o => o.GetListAsync<Category>())
                .ReturnsAsync(categories);

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            controller = new CategoriesController(unitOfWorkMock.Object);
            // Act
            int categoriesCount = (await controller.GetAllGategoriesAsync()).Count;
            // Assert
            Assert.Equal(4, categoriesCount);
        }
    }
}
