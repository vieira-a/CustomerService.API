namespace API.Presenter.Responses;

public sealed class DatabaseProblemDetails : ProblemDetails
{
    public DatabaseProblemDetails()
    {
        Title = "DATABASE";
        Status = StatusCodes.Status500InternalServerError;
    }
}