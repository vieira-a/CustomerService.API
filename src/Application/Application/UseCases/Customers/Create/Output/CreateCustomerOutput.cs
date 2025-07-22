namespace Application.UseCases.Customers.Create.Output;

public record CreateCustomerOutput(Guid CustomerId, string Name, string Email);