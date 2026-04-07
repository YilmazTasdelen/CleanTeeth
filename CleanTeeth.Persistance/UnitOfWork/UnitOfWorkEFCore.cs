using CleanTeeth.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Persistance.UnitOfWork
{
    public class UnitOfWorkEFCore : IUnitOfWork
    {
        private readonly CleanTeethDbContext _context;

        public UnitOfWorkEFCore(CleanTeethDbContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
