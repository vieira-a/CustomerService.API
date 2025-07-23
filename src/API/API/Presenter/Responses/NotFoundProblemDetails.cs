namespace API.Presenter.Responses;

public class NotFoundProblemDetails : ProblemDetails
{
    public NotFoundProblemDetails()
    {
        Title = "Not found";
        Status = StatusCodes.Status404NotFound;
        Detail = "Recurso não encontrado.";
    }
}