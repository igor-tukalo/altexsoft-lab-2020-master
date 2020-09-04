using System.Collections.Generic;

namespace task2.Repositories
{
    abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public List<T> Items { get; set; }
        public BaseRepository(List<T> context)
        {
            Items = context;
        }

        public void Create(T item)
        {
            Items.Add(item);
        }

        public abstract T Get(int id);

        public abstract void Update(T item);

        public abstract void Delete(int id);
    }
}
