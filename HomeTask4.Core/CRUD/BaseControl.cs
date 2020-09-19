using HomeTask4.Core.Repositories;
using HomeTask4.SharedKernel;

namespace HomeTask4.Core.CRUD
{
    public abstract class BaseControl
    {
        protected Validation ValidManager { get; }
        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseControl(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ValidManager = new Validation();
        }
    }
}
