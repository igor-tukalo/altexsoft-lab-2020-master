using HomeTask4.SharedKernel.Interfaces;
using Moq;

namespace HomeTask5.Core.Tests
{
    public class BaseTests
    {
        protected readonly Mock<IRepository> _repositoryMock;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public BaseTests()
        {
            _repositoryMock = new Mock<IRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.SetupGet(o => o.Repository)
                .Returns(_repositoryMock.Object);
        }
    }
}
