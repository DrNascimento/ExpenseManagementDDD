using Application.ViewModel.Category;
using Application.ViewModel.ExpenseType;
using System.Text.Json.Serialization;

namespace Application.ViewModel.Expense
{
    public class ExpenseViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("installments")]
        public ICollection<InstallmentViewModel> ExpenseInstallments { get; set; }

        [JsonPropertyName("category")]
        public CategoryViewModel Category { get; set; }

        [JsonPropertyName("expense_type")]
        public ExpenseTypeViewModel ExpenseType { get; set; }

    }

    public class InstallmentViewModel
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
}
