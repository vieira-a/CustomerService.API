using Application.Interfaces;
using Application.UseCases.Customers.Update.Input;
using Domain.Entities;
using Domain.Repositories;
using Shared.Messaging.Events;

namespace Application.UseCases.Customers.Update;

public class UpdateCustomerInteractor : IUpdateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventPublisher _eventPublisher;

    public UpdateCustomerInteractor(ICustomerRepository customerRepository, IEventPublisher eventPublisher)
    {
        _customerRepository = customerRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task ExecuteAsync(Guid customerId, UpdateCustomerInput? input)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId);
        
        if (customer == null)
            throw new Exception("Customer not found");

        if (input != null) customer.UpdateName(input.Name);
        await _customerRepository.UpdateAsync(customer);

        var customerUpdatedEvent = new CustomerUpdatedEvent
        {
            CustomerId = customerId,
            Name = customer.Name,
        };
        
        await _eventPublisher.Publish(customerUpdatedEvent);
    }
}