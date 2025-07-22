namespace Application.UseCases.Customers.Find.Output;

public record FindCustomerOutput(Guid CustomerId, string Name, string Email);