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
        private readonly Mock<IOptions<CustomSettings>> optionsMock;
        private readonly Mock<IRepository> repositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        public CategoriesControllerTests()
        {
            repositoryMock = new Mock<IRepository>();
            CustomSettings app = new CustomSettings() { NumberConsoleLines = 20 };                                                                       // Make sure you include using Moq;
            optionsMock = new Mock<IOptions<CustomSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(app);
        }

        [Fact]
        public async Task GetByIdCategory_Should_CategoryName()
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

            CategoriesController controller = new CategoriesController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            string categoryName = (await controller.GetByIdAsync(It.IsAny<int>())).Name;

            //Assert
            Assert.Equal("Bakery products", categoryName);
        }

        [Fact]
        public async Task GetItemsWhereParentId_Should_Count()
        {
            // Arrange
            List<Category> categories = new List<Category>
            {
                new Category()
                {
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    ParentId = It.IsAny<int>()
                },
                new Category()
                {
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    ParentId = 2
                },
                new Category()
                {
                    Id = It.IsAny<int>(),
                    Name = It.IsAny<string>(),
                    ParentId = 2
                },
            };

            repositoryMock.Setup(o => o.GetListWhereAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categories.Where(x=>x.ParentId==2).ToList());

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(o => o.Repository)
                .Returns(repositoryMock.Object);

            CategoriesController controller = new CategoriesController(unitOfWorkMock.Object, optionsMock.Object);
            // Act
            int categoriesCount = (await controller.GetItemsWhereParentIdAsync(2)).Count;
            // Assert
            Assert.Equal(2, categoriesCount);
        }

        //[Fact]
        //public async Task AddCategory_Should_NewCategory()
        //{
        //    // Arrange
        //    bool isAdded = false;
        //    repositoryMock.Setup(o => o.AddAsync(It.IsAny<Category>())).Callback(() => isAdded = true);

        //    unitOfWorkMock = new Mock<IUnitOfWork>();
        //    unitOfWorkMock.Setup(o => o.Repository)
        //        .Returns(repositoryMock.Object);

        //     = new CategoriesController(unitOfWorkMock.Object, optionsMock.Object);
        //    // Act
        //    await controller.AddAsync(It.IsAny<string>(), It.IsAny<string>());
        //    // Assert
        //    Assert.True(isAdded);
        //}
    }
}
