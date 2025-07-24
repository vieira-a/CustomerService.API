namespace API.Presenter.Responses;

public sealed class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails()
    {
        Title = "NOT_FOUND";
        Status = StatusCodes.Status404NotFound;
    }
}