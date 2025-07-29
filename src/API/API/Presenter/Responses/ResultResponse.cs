using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Utils;

namespace API.Presenter.Responses;

public static class ResultResponse
{
    private const string UnexpectedExceptionMessage = "Ocorreu um erro inesperado. Tente novamente mais tarde.";

    public static IActionResult ToResponse<T>(
        this Result<T> result,
        HttpContext httpContext,
        int? successStatus = null,
        string? createdResourceLocation = null
        )
    {
        if (result.IsSuccess)
        {
            if (createdResourceLocation is not null)
                return new CreatedResult(createdResourceLocation, result.Value);

            if (typeof(T) == typeof(bool))
                return new StatusCodeResult(successStatus ?? StatusCodes.Status204NoContent);

            return new OkObjectResult(result.Value)
            {
                StatusCode = successStatus ?? StatusCodes.Status200OK
            };
        }

        var instancePath = httpContext.Request.Path;

        return result.ErrorType switch
        {
            EErrorType.Validation => new BadRequestObjectResult(new ValidationProblemDetails
            {
                Instance = instancePath,
                Errors = result.ValidationErrors?.Count > 0 ? result.ValidationErrors.ToList() : [ ]
            }),

            EErrorType.NotFound => new NotFoundObjectResult(new NotFoundProblemDetails
            {
                Detail = result.ErrorMessage,
                Instance = instancePath
            }),

            EErrorType.Database => new ObjectResult(new NotFoundProblemDetails
            {
                Detail = result.ErrorMessage,
                Instance = instancePath
            }),

            EErrorType.Infrastructure => new ObjectResult(new InfrastructureProblemDetails
            {
                Detail = result.ErrorMessage,
                Instance = instancePath
            }),

            EErrorType.Internal => new ObjectResult(new InternalProblemDetails
            {
                Detail = result.ErrorMessage,
                Instance = instancePath
            }),

            _ => new ObjectResult(new InfrastructureProblemDetails
            {
                Detail = result.ErrorMessage ?? UnexpectedExceptionMessage,
                Instance = instancePath,
            })
        };
    }
}