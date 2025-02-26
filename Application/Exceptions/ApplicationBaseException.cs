namespace Application.Exceptions;

public class ApplicationBaseException(string message) : Exception(message)
{
    public int StatusCode { get; set; } = 400;
}