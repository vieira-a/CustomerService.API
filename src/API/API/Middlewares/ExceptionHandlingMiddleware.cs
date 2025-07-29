using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Exceção não manipulada. Caminho: {Path}, Método: {Method}",
                httpContext.Request.Path,
                httpContext.Request.Method);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        const int statusCode = (int)HttpStatusCode.InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Title = "Erro interno do servidor.",
            Status = statusCode,
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(problemDetails);

        await context.Response.WriteAsync(json);
    }
}