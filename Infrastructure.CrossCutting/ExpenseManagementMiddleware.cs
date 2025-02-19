using Microsoft.AspNetCore.Http;
using FluentValidation;
using System.Text.Json;
using Infrastructure.CrossCutting.Models;

namespace Infrastructure.CrossCutting;

public class ExpenseManagementMiddleware
{
    private readonly RequestDelegate _next;

    public ExpenseManagementMiddleware(RequestDelegate next)
    {
        _next = next;
    }

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
                case InvalidOperationException invalidOperationException:

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    result = new ErrorResponse(invalidOperationException.Message);
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
