namespace Application.Exceptions;

public class LoginFailedException : ApplicationBaseException
{
    public LoginFailedException() : base("Invalid e-mail or password. Please try again.")
    {
        StatusCode = 401;
    }
}
