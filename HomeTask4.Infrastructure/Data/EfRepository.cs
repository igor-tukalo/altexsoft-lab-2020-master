using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly AppDbContext _context;
        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<List<T>> GetByCondition<T>(Func<T, bool> predicate) where T : BaseEntity
        {
            return await Task.Run(() => _context.Set<T>().AsNoTracking().Where(predicate).ToList()).ConfigureAwait(false);
        }

        public async Task<List<T>> GetListAsync<T>() where T : BaseEntity
        {
            return await Task.Run(() => _context.Set<T>().ToList()).ConfigureAwait(false);
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
