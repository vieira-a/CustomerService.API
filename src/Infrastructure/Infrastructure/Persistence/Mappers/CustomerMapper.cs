using Domain.Entities;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mappers;

public abstract class CustomerMapper
{
    public static CustomerModel MapFromDomain(Customer customer)
    {
        return new CustomerModel
        {
            CustomerId = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Addresses = customer.Addresses.Select(address => new AddressModel
            {
                AddressId = address.Id == Guid.Empty ? Guid.NewGuid() : address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Country = address.Country,
                IsMain = address.IsMain,
                CustomerId = customer.Id
            }).ToList()
        };
    }

    public static Customer? MapFromEntity(CustomerModel customer)
    {
        return Customer.Restore(customer.CustomerId, customer.Name, customer.Email);
    }
}