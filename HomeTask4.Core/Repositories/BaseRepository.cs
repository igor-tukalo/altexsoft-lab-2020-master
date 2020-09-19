using HomeTask4.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected Validation ValidManager { get; }

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        internal List<T> Items { get; set; }
        public BaseRepository(DbContext context)
        {
            ValidManager = new Validation();

            _context = context;
            if (context != null)
            {
                _dbSet = context.Set<T>();
            }

            Items = GetItems();
        }

        public List<T> GetItems()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            Items = GetItems();
        }

        public T GetItem(int id)
        {
            return _dbSet.Find(id);
        }
        public void Delete(T item, bool isSave = true)
        {
            _dbSet.Remove(item);
            if (isSave)
            {
                _context.SaveChanges();
            }

            Items = GetItems();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            Items = GetItems();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
