namespace API.Presenter.Responses;

public sealed class InfrastructureProblemDetails : ProblemDetails
{
    public InfrastructureProblemDetails()
    {
        Title = "INFRASTRUCTURE";
        Status = StatusCodes.Status502BadGateway;
    }
}