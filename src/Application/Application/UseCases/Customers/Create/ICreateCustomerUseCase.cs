using Application.UseCases.Customers.Create.Input;
using Application.UseCases.Customers.Create.Output;

namespace Application.UseCases.Customers.Create;

public interface ICreateCustomerUseCase
{
    Task<CreateCustomerOutput> ExecuteAsync(CreateCustomerInput input);
}