using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly AppDbContext _context;
        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public T GetById<T>(int id) where T : BaseEntity
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList<T>() where T : BaseEntity
        {
            return _context.Set<T>().ToList();
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
