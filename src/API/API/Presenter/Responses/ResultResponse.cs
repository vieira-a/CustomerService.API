using Microsoft.AspNetCore.Mvc;

namespace API.Presenter.Responses;

public static class ResultResponse
{
    public static IActionResult ToResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(new { message = result.ErrorMessage }),

            ErrorType.Validation => new BadRequestObjectResult(
                new ValidationProblemDetails
                {
                    Title = result.ErrorMessage,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Ocorreram um ou mais erros de validação.",
                    Errors = result.ValidationErrors != null
                        ? result.ValidationErrors.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray())
                        : new Dictionary<string, string[]>()
                }),

            _ => new ObjectResult(new ProblemDetails
            {
                Title = "Ocorreu um erro inesperado.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = result.ErrorMessage,
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }
}