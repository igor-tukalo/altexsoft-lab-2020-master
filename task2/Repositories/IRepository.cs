using System.Collections.Generic;

namespace task2.Repositories
{
    interface IRepository<T> where T : class
    {
        List<T> Items { get; set; }
        abstract T Get(int id);
        void Create(T item);
        abstract void Update(T item);
        abstract void Delete(int id);
    }
}
