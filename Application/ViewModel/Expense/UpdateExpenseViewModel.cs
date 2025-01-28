using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModel.Expense
{
    public class UpdateExpenseViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [JsonPropertyName("category_id")]
        public Guid CategoryId { get; set; }
    }
}
