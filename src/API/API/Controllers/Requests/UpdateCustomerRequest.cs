using Application.UseCases.Customers.Update.Input;

namespace API.Controllers.Requests;

public sealed class UpdateCustomerRequest
{
    public string? Name { get; set; }

    public UpdateCustomerInput? ToInput()
    {
        if (Name != null)
            return new UpdateCustomerInput(
                Name
            );
        return null;
    }
}