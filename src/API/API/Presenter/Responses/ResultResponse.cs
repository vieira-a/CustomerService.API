using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Utils;

namespace API.Presenter.Responses;

public static class ResultResponse
{
    public static IActionResult ToResponse<T>(this Result<T> result, HttpContext httpContext)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        var instancePath = httpContext.Request.Path;

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(new NotFoundProblemDetails
            {
                Instance = instancePath
            }),

            ErrorType.Validation => new BadRequestObjectResult(new ValidationProblemDetails
            {
                Instance = instancePath,
                Errors = result.ValidationErrors != null
                    ? result.ValidationErrors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray())
                    : new Dictionary<string, string[]>()
            }),
            _ => new OkObjectResult(new { message = result.ErrorMessage ?? "Erro interno" })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }
}