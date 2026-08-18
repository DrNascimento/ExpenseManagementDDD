namespace Application.DTO.Input.ExpenseInstallment;

public record UpdateExpenseInstallmentInput
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    [JsonPropertyName("duedate")]
    public DateTime DueDate { get; init; }

    [JsonPropertyName("is_paid")]
    public bool IsPaid { get; init; }
}
