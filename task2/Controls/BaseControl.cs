using task2.Interfaces;

namespace task2.Controls
{
    public abstract class BaseControl : IBaseControl
    {
        public UnitOfWork UnitOfWork { get; set; }

        protected BaseControl()
        {
            UnitOfWork = new UnitOfWork();
        }
        public virtual void Add()
        {
            UnitOfWork.SaveAllData();
        }
        public virtual void Edit(int id)
        {
            UnitOfWork.SaveAllData();
        }
        public virtual void Delete(int id)
        {
            UnitOfWork.SaveAllData();
        }
    }
}
