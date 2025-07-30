namespace Infrastructure.Persistence.Models;

public sealed class AddressModel : BaseModel
{
    public Guid AddressId { get; init; }
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string Country { get; init; }
    public bool IsMain { get; init; }
    public Guid CustomerId { get; init; }
    public CustomerModel? Customer { get; init; }

    private AddressModel(string street, string city, string state, string zipCode, string country, bool isMain, Guid customerId)
    {
        AddressId = Guid.NewGuid();
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        IsMain = isMain;
        CustomerId = customerId;
    }
    
    private AddressModel(Guid addressId, string street, string city, string state, string zipCode, string country, bool isMain, Guid customerId)
    {
        AddressId = addressId;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        IsMain = isMain;
        CustomerId = customerId;
    }

    public static AddressModel Create(string street, string city, string state, string zipCode, string country, bool isMain, Guid customerId)
    {
        return new AddressModel(street, city, state, zipCode, country, isMain, customerId);
    }

    public static AddressModel Restore(
        Guid addressId,
        string street,
        string city,
        string state,
        string zipCode,
        string country,
        bool isMain,
        Guid customerId
        )
    {
        return new AddressModel(addressId, street, city, state, zipCode, country, isMain, customerId);
    }
}