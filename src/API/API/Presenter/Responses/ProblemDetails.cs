namespace API.Presenter.Responses;

public class ProblemDetails
{
    public string Type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
    public string Title { get; set; } = "Erro";
    public int Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }

    public virtual Dictionary<string, string[]>? Errors { get; set; }
}