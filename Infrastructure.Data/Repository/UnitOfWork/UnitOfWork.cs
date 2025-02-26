using Domain.Interfaces.UnitOfWork;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.Repository.UnitOfWork;

public class UnitOfWork(ExpenseManagementContext context) : IUnitOfWork
{
    private IDbContextTransaction _transaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task<bool> CommitAsync() 
        => await context.SaveChangesAsync() > 0;

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
       => await context.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        => await _transaction.RollbackAsync(cancellationToken);
}
