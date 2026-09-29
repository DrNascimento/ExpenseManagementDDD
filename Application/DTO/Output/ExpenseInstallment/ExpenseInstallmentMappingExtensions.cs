using Application.DTO.Output.Category;
using Application.DTO.Output.ExpenseType;

namespace Application.DTO.Output.ExpenseInstallment;

public static class ExpenseInstallmentMappingExtensions
{
    public static ExpenseInstallmentOutput ToOutput(this Domain.Entities.ExpenseInstallment installment) =>
        new()
        {
            Id = installment.Id,
            Expense = installment.Expense?.ToExpenseToInstallmentOutput(),
            InstallmentNumber = installment.InstallmentNumber,
            DueDate = installment.DueDate,
            Amount = installment.Amount,
            IsPaid = installment.IsPaid
        };

    public static IEnumerable<ExpenseInstallmentOutput> ToOutput(this IEnumerable<Domain.Entities.ExpenseInstallment> installments) =>
        installments.Select(ToOutput);

    public static ExpenseToInstallmentOutput ToExpenseToInstallmentOutput(this Domain.Entities.Expense expense) =>
        new()
        {
            Id = expense.Id,
            Name = expense.Name,
            Category = expense.Category?.ToOutput(),
            ExpenseType = expense.ExpenseType?.ToOutput()
        };
}
