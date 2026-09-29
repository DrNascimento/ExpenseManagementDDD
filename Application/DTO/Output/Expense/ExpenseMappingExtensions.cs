using Application.DTO.Output.Category;
using Application.DTO.Output.ExpenseType;

namespace Application.DTO.Output.Expense;

public static class ExpenseMappingExtensions
{
    public static ExpenseOutput ToOutput(this Domain.Entities.Expense expense) =>
        new()
        {
            Id = expense.Id,
            Name = expense.Name,
            ExpenseInstallments = expense.ExpenseInstallments.Select(ToOutput).ToList(),
            Category = expense.Category?.ToOutput(),
            ExpenseType = expense.ExpenseType?.ToOutput()
        };

    public static IEnumerable<ExpenseOutput> ToOutput(this IEnumerable<Domain.Entities.Expense> expenses) =>
        expenses.Select(ToOutput);

    public static InstallmentOutput ToOutput(this Domain.Entities.ExpenseInstallment installment) =>
        new()
        {
            Id = installment.Id,
            InstallmentNumber = installment.InstallmentNumber,
            DueDate = installment.DueDate,
            Amount = installment.Amount,
            IsPaid = installment.IsPaid
        };
}
