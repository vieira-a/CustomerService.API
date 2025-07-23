namespace API.Presenter.Responses;

public class ValidationProblemDetails
{
    public string? Type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
    
    public string Title { get; set; } = "Validation Error";
    
    public int Status { get; set; } = StatusCodes.Status400BadRequest;
    
    public string Detail { get; set; } = "Ocorreram um ou mais erros de validação.";
    
    public Dictionary<string, string[]> Errors { get; set; } = new();
}