using task2.Repositories;

namespace task2.Interfaces
{
    public interface IBaseControl
    {
        public CookBookContext DBControl { get; set; }
        public virtual void Add() { }
        public virtual void Delete(int id) { }
        public virtual void Edit(int id) { }
    }
}
