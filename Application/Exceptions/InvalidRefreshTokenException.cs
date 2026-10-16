namespace Application.Exceptions;

public class InvalidRefreshTokenException : ApplicationBaseException
{
    public InvalidRefreshTokenException() : base("Invalid or expired refresh token. Please log in again.")
    {
        StatusCode = 401;
    }
}
