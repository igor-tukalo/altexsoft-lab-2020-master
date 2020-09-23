using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;

namespace HomeTask4.Core.CRUD
{
    public abstract class BaseControl
    {
        protected Validation ValidManager { get; }
        protected IUnitOfWork UnitOfWork { get; }

        public BaseControl(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ValidManager = new Validation();
        }
    }
}
