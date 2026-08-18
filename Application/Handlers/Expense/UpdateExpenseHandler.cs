using Application.DTO.Input.Expense;

namespace Application.Handlers.Expense;

public class UpdateExpenseHandler(
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateExpenseInput> validator)
{
    public async Task ExecuteAsync(Guid id, UpdateExpenseInput input, Guid userId)
    {
        input.Id = id;
        await validator.ValidateAndThrowAsync(input);

        await ValidatePermission(id, userId);

        var expense = await expenseRepository.GetById(id)
            ?? throw new ResourceNotFoundException("Expense not found.");

        expense.Name = input.Name;
        expense.CategoryId = input.CategoryId;

        expenseRepository.Update(expense);
        await unitOfWork.CommitAsync();
    }

    private async Task ValidatePermission(Guid id, Guid userId)
    {
        var expense = await expenseRepository.GetById(id);
        if (expense is null || userId != expense.UserId)
            throw new InvalidOperationException("Expense not found");
    }
}
