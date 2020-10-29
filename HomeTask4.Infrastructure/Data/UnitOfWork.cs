using HomeTask4.Core;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
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

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
