using System.Text.Json.Serialization;

namespace Application.ViewModel.Category;

public class CreateCategoryViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonIgnore]
    public Guid? UserId { get; set; }
}
