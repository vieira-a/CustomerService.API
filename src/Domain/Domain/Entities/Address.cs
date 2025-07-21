namespace Domain.Entities;

public class Address : Entity
{
    public string Street { get; private set; }
    
    public string City { get; private set; }
    
    public string State { get; private set; }
    
    public string ZipCode { get; private set; }
    
    public string Country { get; private set; }

    private Address () {}

    public Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public static Address Create(string street, string city, string state, string zipCode, string country)
    {
        return new Address(street, city, state, zipCode, country);
    }
}