using Domain.Exceptions;

namespace Domain.Entities;

public sealed class Address : Entity
{
    public string Street { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }

    public string ZipCode { get; private set; }

    public string Country { get; private set; }

    private const string EmptyStreetException = "O nome da rua é obrigatório.";
    private const string EmptyCityException = "O nome da cidade é obrigatório.";
    private const string EmptyZipCodeException = "O CEP é obrigatório.";
    private const string EmptyCountryException = "O nome do país é obrigatório.";
    private const string EmptyStateException = "O nome do estado é obrigatório.";

    private Address(string street, string city, string state, string zipCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Validate();
    }

    public static Address Create(string street, string city, string state, string zipCode, string country)
    {
        return new Address(street, city, state, zipCode, country);
    }

    private void Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Street)) errors.Add(EmptyStreetException);

        if (string.IsNullOrWhiteSpace(City)) errors.Add(EmptyCityException);

        if (string.IsNullOrWhiteSpace(State)) errors.Add(EmptyStateException);

        if (string.IsNullOrWhiteSpace(ZipCode)) errors.Add(EmptyZipCodeException);

        if (string.IsNullOrWhiteSpace(Country)) errors.Add(EmptyCountryException);

        if (errors.Count != 0) throw new DomainValidationException(errors);
    }
}