using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Web.Models
{
    public class ValidationFailureResponse
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; } = true;

        [JsonPropertyName("errors")]
        public IEnumerable<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

    }

    public class ValidationFailure
    {

        [JsonPropertyName("property")]
        public string PropertyName { get; set; }

        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }
    }
}
