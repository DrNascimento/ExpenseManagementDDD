namespace Application.DTO.Input.Expense;

public record CreateExpenseInput
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("expense_type_id")]
    public Guid ExpenseTypeId { get; init; }

    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; init; }

    [JsonPropertyName("number_installments")]
    public int Installments { get; init; }

    [JsonPropertyName("expense_installment_ammount")]
    public double ExpenseInstallmentAmount { get; init; }

    [JsonPropertyName("expense_installment_due_date")]
    public DateTime ExpenseInstallmentDueDate { get; init; }
}
