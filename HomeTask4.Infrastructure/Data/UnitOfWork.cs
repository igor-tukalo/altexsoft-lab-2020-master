﻿using HomeTask4.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace HomeTask4.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository Repository { get; }

        public UnitOfWork(AppDbContext context, IRepository repository)
        {
            _context = context;
            Repository = repository;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
