namespace API.Presenter.Responses;

public sealed class InternalProblemDetails : ProblemDetails
{
    public InternalProblemDetails()
    {
        Title = "INTERNAL";
        Status = StatusCodes.Status500InternalServerError;
    }
}