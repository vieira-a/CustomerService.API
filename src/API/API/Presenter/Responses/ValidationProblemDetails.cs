namespace API.Presenter.Responses;

public class ValidationProblemDetails
{
    public string? Type { get; set; } = null;
    public string Title { get; set; } = "Validation Error";
    public int Status { get; set; } = StatusCodes.Status400BadRequest;
    public string Detail { get; set; } = "Ocorreram um ou mais erros de validação.";
    public Dictionary<string, string[]> Errors { get; set; } = new();
}