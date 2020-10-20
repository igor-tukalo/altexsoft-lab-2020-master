using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Data.SqlClient;
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

        public IQueryable<T> GetAllItems<T>() where T : BaseEntity
        {
            return _context.Set<T>().AsQueryable();
        }

        public Task<T> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return GetAllItems<T>().SingleOrDefaultAsync(predicate);
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return GetAllItems<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<T>> GetListAsync<T>() where T : BaseEntity
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> GetListWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return GetAllItems<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                _context.Set<T>().Update(entity);
                return _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                _context.Set<T>().Remove(entity);
                return _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}