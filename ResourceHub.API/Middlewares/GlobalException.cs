using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Exceptions;
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
            context.Response.StatusCode = exception switch
            {
                NotFoundException => 404,
                UnAuthenticatedException => 401,
                ArgumentException => 400,
                DuplicateValueException => 409,
                VaildateException=>422,
                _ => 500
            };

            await context.Response.WriteAsJsonAsync(new ProblemDetails 
            { 
                Status = context.Response.StatusCode, 
                Detail = exception.Message 
            });
        }
    }
}