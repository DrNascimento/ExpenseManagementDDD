namespace Application.Handlers.ExpenseInstallment;

public class DeleteExpenseInstallmentHandler(
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(Guid id, Guid userId)
    {
        if (!expenseInstallmentRepository.HasByUserIdAndId(id, userId))
            throw new InvalidOperationException("Installment not found");

        var installment = await expenseInstallmentRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Installment not found");

        expenseInstallmentRepository.Delete(installment);
        await unitOfWork.CommitAsync();
    }
}