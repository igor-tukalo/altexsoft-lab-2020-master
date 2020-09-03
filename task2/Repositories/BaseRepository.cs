using System.Collections.Generic;

namespace task2.Repositories
{
    class BaseRepository<T> where T : class
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
    }
}
