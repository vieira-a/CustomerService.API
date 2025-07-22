using Application.UseCases.Customers.Update.Input;

namespace Application.UseCases.Customers.Update;

public interface IUpdateCustomerUseCase
{
    Task ExecuteAsync(Guid customerId, UpdateCustomerInput? input);
}