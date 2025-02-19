using Domain.Interfaces.UnitOfWork;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseManagementContext _context;

        private IDbContextTransaction _transaction;

        public UnitOfWork(ExpenseManagementContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
            => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        public async Task<bool> CommitAsync() 
            => await _context.SaveChangesAsync() > 0;

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
           => await _context.Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackAsync()
            => await _transaction.RollbackAsync();

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => await _transaction.RollbackAsync(cancellationToken);
    }
}
