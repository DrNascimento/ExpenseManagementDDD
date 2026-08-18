namespace Application.Handlers.ExpenseInstallment;

public class TogglePaidExpenseInstallmentHandler(
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(Guid id, Guid userId)
    {
        if (!expenseInstallmentRepository.HasByUserIdAndId(id, userId))
            throw new InvalidOperationException("Installment not found");

        var installment = await expenseInstallmentRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Installment not found");

        installment.IsPaid = !installment.IsPaid;
        expenseInstallmentRepository.Update(installment);
        await unitOfWork.CommitAsync();
    }
}