using System.Net;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using OperationalAdministrator.Common;

namespace OperationalAdministrator.Middlewares
{
    public class ExeptionMiddleware : IMiddleware
    {
        private ILogger<ExeptionMiddleware> _logger;
        private OperationalAdministratorContext _context;
        public ExeptionMiddleware(ILogger<ExeptionMiddleware> logger, OperationalAdministratorContext context ) 
        {
            _logger = logger;
            _context = context;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong");
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            ErrorResponse errorResponse = new ErrorResponse()
            {
                StatusCode = statusCode,
                Message = ex.Message
            };
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }

    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExeptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExeptionMiddleware>();
        }
    }

}
