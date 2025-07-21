using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.CreateCustomer;

public class CreateCustomerInteractor : ICreateCustomerUseCase 
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerInteractor(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<CreateCustomerOutput> ExecuteAsync(CreateCustomerInput input)
    {
        var customer = Customer.Create(input.Name, input.Email);
        
        if (input.Address != null)
        {
            var address = Address.Create(
                input.Address.Street, 
                input.Address.City, 
                input.Address.State,
                input.Address.ZipCode, 
                input.Address.Country);  
            
            customer.AddAddress(address);
        }
        
        await _repository.CreateAsync(customer);
        return new CreateCustomerOutput(customer.Id, customer.Name, customer.Email);
    }
}