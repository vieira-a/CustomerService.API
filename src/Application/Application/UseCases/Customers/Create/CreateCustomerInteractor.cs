using Application.UseCases.Customers.Create.Input;
using Application.UseCases.Customers.Create.Output;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Customers.Create;

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
            var address = CreateAddress(input.Address); 
            customer.AddAddress(address);
        }
        
        await _repository.CreateAsync(customer);
        return new CreateCustomerOutput(customer.Id, customer.Name, customer.Email);
    }

    private static Address CreateAddress(AddressInput input)
    {
        return Address.Create(input.Street, input.City, input.State, input.ZipCode, input.Country);
    }
}