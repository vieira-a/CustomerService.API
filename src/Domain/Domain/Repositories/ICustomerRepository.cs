using Domain.Entities;
using Shared.Utils;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Result> CreateAsync(Customer customer);
    Task<Customer?> FindByIdAsync(Guid customerId);
    Task UpdateAsync(Customer customer);
}