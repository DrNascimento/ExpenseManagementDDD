using Application.DTO.Input.ExpenseInstallment;

namespace Application.Handlers.ExpenseInstallment;

public class UpdateExpenseInstallmentHandler(
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateExpenseInstallmentInput> validator)
{
    public async Task ExecuteAsync(Guid id, UpdateExpenseInstallmentInput input, Guid userId)
    {
        input.Id = id;
        await validator.ValidateAndThrowAsync(input);

        if (!expenseInstallmentRepository.HasByUserIdAndId(id, userId))
            throw new InvalidOperationException("Installment not found");

        if (expenseInstallmentRepository.HasOneInstallmentByMonth(id, input.DueDate))
            throw new FluentValidation.ValidationException("An installment already exists for this month.");

        var installment = await expenseInstallmentRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Installment not found");

        installment.DueDate = input.DueDate;
        installment.Amount = input.Amount;
        installment.IsPaid = input.IsPaid;

        expenseInstallmentRepository.Update(installment);
        await unitOfWork.CommitAsync();
    }
}
