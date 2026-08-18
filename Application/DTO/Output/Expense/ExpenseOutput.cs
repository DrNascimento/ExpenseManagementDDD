using Application.DTO.Output.Category;
using Application.DTO.Output.ExpenseType;

namespace Application.DTO.Output.Expense;

public class ExpenseOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("installments")]
    public ICollection<InstallmentOutput> ExpenseInstallments { get; set; } = [];

    [JsonPropertyName("category")]
    public CategoryOutput? Category { get; set; }

    [JsonPropertyName("expense_type")]
    public ExpenseTypeOutput? ExpenseType { get; set; }
}

public class InstallmentOutput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("installment_number")]
    public int InstallmentNumber { get; set; }

    [JsonPropertyName("duedate")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("is_paid")]
    public bool IsPaid { get; set; }
}
