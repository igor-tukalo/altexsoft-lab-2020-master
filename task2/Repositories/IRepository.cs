using System.Collections.Generic;

namespace task2.Repositories
{
    interface IRepository<T> where T : class
    {
        List<T> Items { get; set; }
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}