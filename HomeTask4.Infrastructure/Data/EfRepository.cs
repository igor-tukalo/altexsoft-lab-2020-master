using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private IQueryable<T> GetAllItems<T>() where T : BaseEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> GetListAsync<T>() where T : BaseEntity
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetListWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return await GetAllItems<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
