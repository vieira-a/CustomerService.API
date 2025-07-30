using Domain.Entities;
using Shared.Utils;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<Result> CreateAsync(Customer customer);
    Task<Result<Customer?>> FindByIdAsync(Guid customerId);
    Task<Result<bool>> UpdateAsync(Customer customer);
    Task<Result<bool>> DeleteAsync(Guid customerId);
    Task<Customer?> FindWithAddressAsync(Guid customerId);
    Task<Result> CreateAddressAsync(Guid customerId, Address address);
}