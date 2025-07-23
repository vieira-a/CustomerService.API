using Domain.Entities;
using Shared.Utils;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Result> CreateAsync(Customer customer);
    Task<Result<Customer?>> FindByIdAsync(Guid customerId);
    Task<Result> UpdateAsync(Customer customer);
}