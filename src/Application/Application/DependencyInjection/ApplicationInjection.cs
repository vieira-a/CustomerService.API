using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.UseCases.Customers.Create;
using Application.UseCases.Customers.CreateAddress;
using Application.UseCases.Customers.Delete;
using Application.UseCases.Customers.Find;
using Application.UseCases.Customers.Update;

namespace Application.DependencyInjection;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerInteractor>();
        services.AddScoped<IFindCustomerUseCase, FindCustomerInteractor>();
        services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerInteractor>();
        services.AddScoped<IDeleteCustomerUseCase, DeleteCustomerInteractor>();
        services.AddScoped<ICreateAddressUseCase, CreateAddressInteractor>();

        return services;
    }

}