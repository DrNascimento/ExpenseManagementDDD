using Application.DTO.Output.Category;
using Application.DTO.Output.ExpenseType;

namespace Application.DTO.Output.ExpenseInstallment;

public class ExpenseInstallmentOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("expense")]
    public ExpenseToInstallmentOutput? Expense { get; set; }

    [JsonPropertyName("installment_number")]
    public int InstallmentNumber { get; set; }

    [JsonPropertyName("duedate")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("ammount")]
    public double Amount { get; set; }

    [JsonPropertyName("is_paid")]
    public bool IsPaid { get; set; }
}

public class ExpenseToInstallmentOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public CategoryOutput? Category { get; set; }

    [JsonPropertyName("expense_type")]
    public ExpenseTypeOutput? ExpenseType { get; set; }
}
