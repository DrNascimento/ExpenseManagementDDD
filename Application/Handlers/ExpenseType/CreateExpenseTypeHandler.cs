using Application.DTO.Input.ExpenseType;

namespace Application.Handlers.ExpenseType;

public class CreateExpenseTypeHandler(
    IExpenseTypeRepository expenseTypeRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateExpenseTypeInput> validator)
{
    public async Task<Guid> ExecuteAsync(CreateExpenseTypeInput input)
    {
        await validator.ValidateAndThrowAsync(input);

        var expenseType = new Domain.Entities.ExpenseType
        {
            Name = input.Name,
            IsFixed = input.IsFixed
        };

        expenseTypeRepository.Add(expenseType);
        await unitOfWork.CommitAsync();

        return expenseType.Id;
    }
}
