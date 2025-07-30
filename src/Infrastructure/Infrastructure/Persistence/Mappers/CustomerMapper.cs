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
            Addresses = customer.Addresses.Select(address =>
                AddressModel.Restore(
                    address.Id == Guid.Empty ? Guid.NewGuid() : address.Id,
                    address.Street,
                    address.City,
                    address.State,
                    address.ZipCode,
                    address.Country,
                    address.IsMain,
                    customer.Id
                )
            ).ToList()
        };
    }

    public static Customer? MapFromEntity(CustomerModel customer)
    {
        var customerEntity = Customer.Restore(customer.CustomerId, customer.Name, customer.Email);

        if (customer.Addresses == null)
            return customerEntity;

        foreach (var addressModel in customer.Addresses)
        {
            var address = Address.Restore(
                addressModel.AddressId,
                addressModel.Street,
                addressModel.City,
                addressModel.State,
                addressModel.ZipCode,
                addressModel.Country,
                addressModel.IsMain);

            customerEntity?.RestoreAddress(address);
        }
        return customerEntity;
    }
}