using task2.Interfaces;

namespace task2.Interfaces
{
    public interface IBaseControl
    {
        public UnitOfWork UnitOfWork { get; set; }
        public virtual void Add() { }
        public virtual void Delete(int id) { }
        public virtual void Edit(int id) { }
    }
}
