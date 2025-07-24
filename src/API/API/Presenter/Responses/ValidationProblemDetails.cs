namespace API.Presenter.Responses;

public class ValidationProblemDetails : ProblemDetails
{
    public ValidationProblemDetails()
    {
        Title = "Validation Error";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Ocorreram um ou mais erros de validação.";
        Errors = new Dictionary<string, string[]>();
    }

    public sealed override Dictionary<string, string[]>? Errors { get; set; }
}