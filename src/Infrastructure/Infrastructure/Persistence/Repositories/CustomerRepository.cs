using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Mappers;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext  _context;

    public CustomerRepository(CustomerDbContext context)
    {
        this._context = context;
    }

    public async Task CreateAsync(Customer customer)
    {
        var customerModel = CustomerMapper.MapFromDomain(customer);
        
        _context.Customers.Add(customerModel);
        await _context.SaveChangesAsync();
    }
}