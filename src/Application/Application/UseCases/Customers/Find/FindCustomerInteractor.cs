using Application.UseCases.Customers.Find.Output;
using Domain.Repositories;

namespace Application.UseCases.Customers.Find;

public class FindCustomerInteractor : IFindCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public FindCustomerInteractor(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<FindCustomerOutput?> ExecuteAsync(Guid customerId)
    {
        var customer = await _customerRepository.FindByIdAsync(customerId);
        return customer == null ? null : new FindCustomerOutput(customer.Id, customer.Name, customer.Email);
    }
}