using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Models
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

        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }
    }
}
