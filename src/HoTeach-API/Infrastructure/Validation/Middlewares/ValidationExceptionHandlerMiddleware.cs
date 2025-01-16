using FluentValidation;
using System.Net;
using System.Text.Json;
using HoTeach.API.Common;

namespace HoTeach.API.Infrastructure.Validation.Middlewares
{
    /// <summary>
    /// Middleware to handle FluentValidation exceptions and return standardized responses.
    /// </summary>
    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = exception.Errors;
            var result = Result.Failure(errors.Select(e => e.ErrorMessage));

            var jsonResponse = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            return context.Response.WriteAsync(jsonResponse);
        }
    }

    /// <summary>
    /// Extension methods to register ValidationExceptionHandler middleware.
    /// </summary>
    public static class ValidationExceptionHandlerExtensions
    {
        /// <summary>
        /// Adds the ValidationExceptionHandler middleware to the application pipeline.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder instance.</param>
        /// <returns>The IApplicationBuilder instance for chaining.</returns>
        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
        }
    }
}
