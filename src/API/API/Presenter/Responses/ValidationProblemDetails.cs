namespace API.Presenter.Responses;

public sealed class ValidationProblemDetails : ProblemDetails
{
    public ValidationProblemDetails()
    {
        Title = "VALIDATION";
        Status = StatusCodes.Status400BadRequest;
        Detail = "Ocorreram um ou mais erros de validação.";
        Errors = [ ];
    }

    public sealed override List<string>? Errors { get; set; }
}