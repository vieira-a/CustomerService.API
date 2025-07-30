using Application.UseCases.Customers.Create.Input;
using Domain.Entities;
using Shared.Utils;

namespace Application.UseCases.Customers.CreateAddress;

public interface ICreateAddressUseCase
{
    Task<Result<bool>> ExecuteAsync(Guid customerId, AddressInput input);
}