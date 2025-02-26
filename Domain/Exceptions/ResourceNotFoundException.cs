using System.Net;

namespace Domain.Exceptions;

public class ResourceNotFoundException(string message) : ExpenseManagementBaseException(message, HttpStatusCode.NotFound)
{
}
