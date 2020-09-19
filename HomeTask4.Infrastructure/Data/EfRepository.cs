using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        public Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
