namespace Application.UseCases.CreateCustomer;

public record CreateCustomerInput(string Name, string Email, AddressInput? Address);
    
public record AddressInput(string Street, string City, string State, string ZipCode, string Country);