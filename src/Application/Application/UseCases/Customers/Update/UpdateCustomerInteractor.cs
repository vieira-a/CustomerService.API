using Application.Interfaces;
using Application.UseCases.Customers.Update.Input;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Shared.Enums;
using Shared.Messaging.Events;
using Shared.Utils;

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

    public async Task<Result> ExecuteAsync(Guid customerId, UpdateCustomerInput? input)
    {
        try
        {
            var result = await _customerRepository.FindByIdAsync(customerId);
        
            if(result.IsFailure)
                return Result.Fail(result.ErrorMessage!, result.ErrorType);
        
            if(result.Value == null)
                return Result.Fail(result.ErrorMessage!, ErrorType.NotFound);

            var customer = result.Value;
        
            if (input != null) customer.UpdateName(input.Name);
            await _customerRepository.UpdateAsync(customer);

            var customerUpdatedEvent = new CustomerUpdatedEvent
            {
                CustomerId = customerId,
                Name = customer.Name,
            };
        
            await _eventPublisher.Publish(customerUpdatedEvent);
            return Result.Success();
        }
        catch (DomainValidationException ex)
        {
            return Result.FailValidation(new Dictionary<string, List<string>>(ex.ValidationErrors));
        }
    }
}