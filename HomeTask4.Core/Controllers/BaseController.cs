using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.Controllers
{
    public abstract class BaseController
    {
        protected Validation ValidManager { get; }
        protected IUnitOfWork UnitOfWork { get; }

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ValidManager = new Validation();
        }
    }
}
