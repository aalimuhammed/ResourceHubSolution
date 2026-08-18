using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ResourceHub.API.Middlewares
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;

        public GlobalException(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context , Exception exception)
        {
            var response = context.Response.ContentType = "application/json";

            if(exception is KeyNotFoundException)
            {
                context.Response.StatusCode = 404;
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = 401;
            }
            else if (exception is ArgumentException)
            {
                context.Response.StatusCode = 400;
            }
            else if (exception is DuplicateNameException)
            {
                context.Response.StatusCode = 409;
            }
            else
            {
                context.Response.StatusCode = 500;
            }

            await context.Response.WriteAsJsonAsync(new ProblemDetails 
            { 
                Status = context.Response.StatusCode, 
                Detail = exception.Message }
            );
            
        }
    }

}
