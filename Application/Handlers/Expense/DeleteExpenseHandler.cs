namespace Application.Handlers.Expense;

public class DeleteExpenseHandler(
    IExpenseRepository expenseRepository,
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var expense = await expenseRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Expense not found.");

        if (expense.UserId != userId)
            throw new InvalidOperationException("Expense not found");

        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            expenseRepository.Delete(expense);
            await expenseInstallmentRepository.DeleteByExpenseId(id);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}