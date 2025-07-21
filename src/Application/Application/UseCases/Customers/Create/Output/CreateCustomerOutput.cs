namespace Application.UseCases.CreateCustomer;

public record CreateCustomerOutput(Guid CustomerId, string Name, string Email);