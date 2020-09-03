using task2.Interfaces;

namespace task2.Controls
{
    abstract class BaseControl : IBaseControl
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseControl(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
