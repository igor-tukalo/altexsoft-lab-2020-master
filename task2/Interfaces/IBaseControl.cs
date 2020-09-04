using task2.Interfaces;

namespace task2.Interfaces
{
    interface IBaseControl
    {
        public virtual void Add() { }
        public virtual void Delete(int id) { }
        public virtual void Edit(int id) { }
    }
}
