using System.Text.Json.Serialization;

namespace Infrastructure.CrossCutting.Models;

public class ErrorResponse
{
    public ErrorResponse(string message)
    {
        Errors = new List<ErrorMessageResponse> { new ErrorMessageResponse(message) };
    }

    public ErrorResponse()
    {

    }

    [JsonPropertyName("error")]
    public bool Error { get; set; } = true;

    [JsonPropertyName("errors")]
    public IEnumerable<ErrorMessageResponse> Errors { get; set; } = new List<ErrorMessageResponse>();

}

public class ErrorMessageResponse
{
    public ErrorMessageResponse(string message)
    {
        Message = message;
    }

    public ErrorMessageResponse()
    {

    }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}
