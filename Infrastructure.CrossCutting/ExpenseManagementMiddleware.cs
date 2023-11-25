using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using FluentValidation;
using System.Text.Json;
using Infrastructure.CrossCutting.Models;

namespace Infrastructure.CrossCutting
{
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
                Debug.WriteLine(ex.Message);

                var result = new ValidationFailureResponse();

                var vf = new ValidationFailure { ErrorMessage = ex.Message };
                result.Errors = new List<ValidationFailure> { vf };

                switch (ex)
                {
                    case ValidationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        result.Errors = ((ValidationException)ex).Errors
                            .Select(e => new ValidationFailure { ErrorMessage = e.ErrorMessage });
                        break;
                    case InvalidOperationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
