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
    public class CategoriesControllerTests : BaseTests
    {
        private readonly CategoriesController _categoriesController;
        private readonly List<Category> _categories;
        private readonly Category _category;
        public CategoriesControllerTests()
        {
            _category = new Category() { Id = 1, Name = "Bakery products", ParentId = 0 };
            _categoriesController = new CategoriesController(_unitOfWorkMock.Object);
            _categories = new List<Category>
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
            _repositoryMock.Setup(o => o.GetByIdAsync<Category>(1))
                .ReturnsAsync(_category);

            // Act
            string categoryName = (await _categoriesController.GetCategoryByIdAsync(1)).Name;

            //Assert
            Assert.Equal("Bakery products", categoryName);
        }

        [Fact]
        public async Task GetItemsWhereParentId_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(_categories.Where(x => x.ParentId == 2).ToList());

            // Act
            int categoriesCount = (await _categoriesController.GetCategoriesWhereParentIdAsync(2)).Count;

            // Assert
            Assert.Equal(4, categoriesCount);
        }

        [Fact]
        public async Task AddCategory_Should_True()
        {
            // Arrange
            _repositoryMock.Setup(o => o.AddAsync(_category));
            _repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(_category);

            // Act
            await _categoriesController.AddAsync("Bakery products", "Categories");

            // Assert
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task RenameCategory_Should_NewName()
        {
            // Arrange
            _repositoryMock.Setup(o => o.UpdateAsync(_category));
            _repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(_category);
            _repositoryMock.Setup(o => o.GetByIdAsync<Category>(1))
                .ReturnsAsync(_category);

            // Act
            await _categoriesController.EditCategoryAsync(1, "Fish", 1);
            string updatedName = (await _categoriesController.GetCategoryByIdAsync(1)).Name;

            // Assert
            Assert.Equal("Fish", updatedName);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task Delete_Should_True()
        {
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
            _repositoryMock.Setup(o => o.DeleteAsync(_category));
            _repositoryMock.Setup(o => o.GetByIdAsync<Category>(2))
                .ReturnsAsync(_category);
            _repositoryMock.Setup(o => o.GetByPredicateAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(_category);

            // Act
            await _categoriesController.DeleteCategoryAsync(2);
            _repositoryMock.Verify();
        }

        [Fact]
        public async Task FindCategories_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(_categories.Where(x => x.Name.ToLower().Contains("a")).ToList());

            // Act
            int categoriesCount = (await _categoriesController.FindCategoriesAsync("a")).Count;

            // Assert
            Assert.Equal(4, categoriesCount);
        }

        [Fact]
        public async Task GetAllGategories_Should_Count()
        {
            // Arrange
            _repositoryMock.Setup(o => o.GetListAsync<Category>())
                .ReturnsAsync(_categories);

            // Act
            int categoriesCount = (await _categoriesController.GetAllGategoriesAsync()).Count;
            // Assert
            Assert.Equal(4, categoriesCount);
        }
    }
}
