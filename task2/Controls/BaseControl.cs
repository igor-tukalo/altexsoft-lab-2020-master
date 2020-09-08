using task2.Interfaces;

namespace task2.Controls
{
    abstract class BaseControl : IBaseControl
    {
        protected IUnitOfWork UnitOfWork { get;set; }

        protected BaseControl(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}