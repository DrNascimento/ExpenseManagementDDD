using System.Text.Json.Serialization;

namespace Application.ViewModel.ExpenseInstallment;

public class UpdateExpenseInstallmentViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("duedate")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("is_paid")]
    public bool IsPaid { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}
