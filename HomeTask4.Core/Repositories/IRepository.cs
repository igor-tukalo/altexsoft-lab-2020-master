using System;
using System.Collections.Generic;

namespace HomeTask4.Core.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T GetItem(int id);
        List<T> GetItems();
        void Create(T item);
        void Update(T item);
        void Delete(T item, bool isSave);
    }
}
