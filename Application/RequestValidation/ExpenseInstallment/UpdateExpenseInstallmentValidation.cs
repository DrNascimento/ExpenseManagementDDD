using Application.DTO.Input.ExpenseInstallment;

namespace Application.RequestValidation.ExpenseInstallment;

public class UpdateExpenseInstallmentValidation : AbstractValidator<UpdateExpenseInstallmentInput>
{
    public UpdateExpenseInstallmentValidation()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.DueDate)
            .NotEmpty();
    }
}
