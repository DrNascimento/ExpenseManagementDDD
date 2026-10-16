using System.Net;

namespace Domain.Exceptions;

public class ConcurrencyConflictException(Exception innerException)
    : ExpenseManagementBaseException("The data was changed by another request. Please try again.", HttpStatusCode.Conflict, innerException)
{
}
