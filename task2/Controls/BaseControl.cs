using task2.Interfaces;
using task2.Repositories;

namespace task2.Controls
{
    public abstract class BaseControl : IBaseControl
    {
        public CookBookContext DBControl { get; set; }
        protected BaseControl()
        {
            DBControl = new CookBookContext();
        }
        public virtual void Add()
        {
            DBControl.SaveAllData();
        }
        public virtual void Edit(int id)
        {
            DBControl.SaveAllData();
        }
        public virtual void Delete(int id)
        {
            DBControl.SaveAllData();
        }
    }
}
