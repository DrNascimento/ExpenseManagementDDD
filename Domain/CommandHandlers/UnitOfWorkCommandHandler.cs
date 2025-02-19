using Domain.Interfaces.UnitOfWork;

namespace Domain.CommandHandlers;

public abstract class UnitOfWorkCommandHandler
{
    protected IUnitOfWork _uow;

    public UnitOfWorkCommandHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    protected async Task CommitAsync()
    {
        await _uow.CommitAsync();
    }

    protected async Task RollbackAsync()
    {
        await _uow.RollbackAsync();
    }

    protected async Task BeginTransactionAsync()
    {
        await _uow.BeginTransactionAsync();
    }

    protected async Task CommitTransactionAsync()
    {
        await _uow.CommitTransactionAsync();
    }

    protected async Task RollbackTransactionAsync()
    {
        await _uow.RollbackTransactionAsync();
    }
}
