using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModel.Expense
{
    public class CreateExpenseViewModel 
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [JsonPropertyName("expense_type_id")]
        [Required(ErrorMessage = "Expense Type is required")]
        public int ExpenseTypeId { get; set; }

        [JsonPropertyName("category_id")]
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [JsonPropertyName("number_installments")]
        [Required(ErrorMessage = "Number of Installments is required")]
        public int Installments { get; set; }

        [JsonPropertyName("expense_installment_ammount")]
        [Required(ErrorMessage = "Ammount is required")]
        public double ExpenseInstallmentAmmount { get; set; }

        [JsonPropertyName("expense_installment_due_date")]
        [Required(ErrorMessage = "Due Date is required")]
        public DateTime ExpenseInstallmentDueDate { get; set; }
    }
}
