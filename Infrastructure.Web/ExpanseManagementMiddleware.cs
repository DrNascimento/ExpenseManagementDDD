using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Web.Models;
using System.Text.Json;

namespace Infrastructure.Web
{
    public class ExpanseManagementMiddleware
    {
        private readonly RequestDelegate _next;

        public ExpanseManagementMiddleware(RequestDelegate next)
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
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                if (ex is ValidationException)
                {

                    var result = new ValidationFailureResponse();
                    result.Errors = (ex as ValidationException)
                        .Errors
                        .Select(e => new ValidationFailure
                        {
                            PropertyName = e.PropertyName,
                            ErrorMessage = e.ErrorMessage
                        });
                    
                    

                    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
                }


            }
        }
    }
}
