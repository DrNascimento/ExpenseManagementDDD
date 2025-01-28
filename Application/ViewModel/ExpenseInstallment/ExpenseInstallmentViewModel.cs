using Application.ViewModel.Category;
using Application.ViewModel.Expense;
using Application.ViewModel.ExpenseType;
using System.Text.Json.Serialization;

namespace Application.ViewModel.ExpenseInstallment
{
    public class ExpenseInstallmentViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("expense")]
        public ExpenseToInstallment Expense { get; set; }

        [JsonPropertyName("installment_number")]
        public int InstallmentNumber { get; set; }

        [JsonPropertyName("duedate")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("ammount")]
        public double Amount { get; set; }

        [JsonPropertyName("is_paid")]
        public bool IsPaid { get; set; }
    }

    public class ExpenseToInstallment
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("category")]
        public CategoryViewModel Category { get; set; }

        [JsonPropertyName("expense_type")]
        public ExpenseTypeViewModel ExpenseType { get; set; }
    }
}
