using System.Collections.Generic;
using System.Linq;
using task2.Models;

namespace task2.Repositories
{
    abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity<int>
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

        public T Get(int id) 
        {
            return (from a in Items
                    where a.Id == id
                    select a).FirstOrDefault();
        }
        public void Delete(int id)
        {
            var item = Get(id);
            if (item != null)
                Items.Remove(item);
        }

        public abstract void Update(T item);
    }
}
