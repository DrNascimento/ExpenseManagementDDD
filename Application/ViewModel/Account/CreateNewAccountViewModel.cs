using System.Text.Json.Serialization;

namespace Application.ViewModel.Account
{
    public class CreateNewAccountViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("confirm_password")]
        public string ConfirmPassword { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("type_user")]
        public int UserTypeEnum { get; set; }

    }
}
