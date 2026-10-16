using System.Net;

namespace Domain.Exceptions;

public class ExpenseManagementBaseException : Exception
{
    public int StatusCode { get; private set;  }

    public ExpenseManagementBaseException (string message, HttpStatusCode httpStatusCode) : base(message)
    {
        StatusCode = (int)httpStatusCode;
    }

    public ExpenseManagementBaseException(string message) : base(message)
    { }

    public ExpenseManagementBaseException(string message, HttpStatusCode httpStatusCode, Exception innerException) : base(message, innerException)
    {
        StatusCode = (int)httpStatusCode;
    }
}
