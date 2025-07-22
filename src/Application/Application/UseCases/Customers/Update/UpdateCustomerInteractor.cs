using Application.UseCases.Customers.Update.Input;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.Customers.Update;

public class UpdateCustomerInteractor : IUpdateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerInteractor(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task ExecuteAsync(Guid customerId, UpdateCustomerInput? input)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId);
        
        if (customer == null)
            throw new Exception("Customer not found");

        customer.UpdateName(input.Name);
        await _customerRepository.UpdateAsync(customer);
    }
}