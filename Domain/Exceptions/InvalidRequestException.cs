using System.Net;

namespace Domain.Exceptions;

public class InvalidRequestException(string field, string message) : ExpenseManagementBaseException($"'{field}' {message}", HttpStatusCode.BadRequest)
{

}
