using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task CreateAsync(Customer customer);
}