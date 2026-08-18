using Application.DTO.Input.Expense;

namespace Application.Handlers.Expense;

public class CreateExpenseHandler(
    IExpenseRepository expenseRepository,
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateExpenseInput> validator)
{
    public async Task<Guid> ExecuteAsync(CreateExpenseInput input, Guid userId, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(input, cancellationToken);

        try
        {
            var expense = new Domain.Entities.Expense
            {
                UserId = userId,
                Name = input.Name,
                ExpenseTypeId = input.ExpenseTypeId,
                CategoryId = input.CategoryId,
                Installments = input.Installments
            };

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            expenseRepository.Add(expense);
            expenseRepository.SaveChanges();

            await SaveExpenseInstallmentsAsync(expense.Id, input);

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return expense.Id;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task SaveExpenseInstallmentsAsync(Guid expenseId, CreateExpenseInput input)
    {
        List<Domain.Entities.ExpenseInstallment> expenseInstallments = [];

        for (var i = 0; i < input.Installments; i++)
        {
            expenseInstallments.Add(new Domain.Entities.ExpenseInstallment
            {
                ExpenseId = expenseId,
                InstallmentNumber = i + 1,
                Amount = input.ExpenseInstallmentAmount,
                DueDate = input.ExpenseInstallmentDueDate.AddMonths(i),
                IsPaid = false
            });
        }

        await expenseInstallmentRepository.Add(expenseInstallments);
        expenseInstallmentRepository.SaveChanges();
    }
}
