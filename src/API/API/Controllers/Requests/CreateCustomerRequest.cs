using Application.UseCases.Customers.Create.Input;

namespace API.Controllers.Requests;

public sealed class CreateCustomerRequest
{
    public required string Name { get; set; }
    
    public required string Email { get; set; }
    
    public AddressInput? Address { get; set; }
    
    public CreateCustomerInput ToInput() => new(
        Name,
        Email,
        Address
    );
}