using FluentValidation;
using System; // Importar System
using System.Net;
using System.Text.Json;

namespace EjercicioGaelZarate.Middleware
{
    public class ErrorHandlingMiddleware
    {
        // ... (El constructor es el mismo) ...
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 by default
            var errors = new List<string> { exception.Message };

            // Manejo de Validaciones de FluentValidation
            if (exception is ValidationException validationException)
            {
                code = HttpStatusCode.BadRequest; // 400
                errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
            }
            // AÑADIMOS ESTO:
            else if (exception is UnauthorizedAccessException unauthorizedException)
            {
                code = HttpStatusCode.Unauthorized; // 401
                errors = new List<string> { unauthorizedException.Message };
            }
            // FIN DE LA ADICIÓN

            var result = JsonSerializer.Serialize(new { errors });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}