using Microsoft.AspNetCore.Http;
using FluentValidation;
using System.Text.Json;
using Infrastructure.CrossCutting.Models;
using Domain.Exceptions;
using Application.Exceptions;

namespace Infrastructure.CrossCutting;

public class ExpenseManagementMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var result = new ErrorResponse();

            switch (ex)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    result.Errors = validationException
                            .Errors
                            .Select(e => new ErrorMessageResponse(e.ErrorMessage));

                    break;
                case ExpenseManagementBaseException expenseManagementBaseException:

                    context.Response.StatusCode = expenseManagementBaseException.StatusCode;

                    result = new ErrorResponse(expenseManagementBaseException.Message);
                    break;
                case ApplicationBaseException applicationBaseException:
                    context.Response.StatusCode = applicationBaseException.StatusCode;

                    result = new ErrorResponse(applicationBaseException.Message);
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    result = new ErrorResponse("Oops! One error occured, please try again later.");
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
