using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.ViewModel.Category
{
    public class CreateCategoryViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }
    }
}
