using Domain.Entities;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Mappers;

public abstract class AddressMapper
{
    public static AddressModel MapFromDomain(Address address, Guid customerId)
    {
        return AddressModel.Restore(
            address.Id,
            address.Street,
            address.City,
            address.State,
            address.ZipCode,
            address.Country,
            address.IsMain,
            customerId); ;
    }
}