using Domain.Exceptions;

namespace Domain.Entities;

public sealed class Address : Entity
{
    public string Street { get; private set; }
    
    public string City { get; private set; }
    
    public string State { get; private set; }
    
    public string ZipCode { get; private set; }
    
    public string Country { get; private set; }

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
        var errors = new Dictionary<string, List<string>>();
        
        if (string.IsNullOrWhiteSpace(Street))
            errors.Add("Street", [ "Rua deve ser informada." ]);
        
        if (string.IsNullOrWhiteSpace(City))
            errors.Add("City", [ "Cidade deve ser informada." ]);
        
        if (string.IsNullOrWhiteSpace(State))
            errors.Add("State", [ "Estado deve ser informado." ]);
        
        if (string.IsNullOrWhiteSpace(ZipCode))
            errors.Add("ZipCode", [ "CEP deve ser informado." ]);
        
        if (string.IsNullOrWhiteSpace(Country))
            errors.Add("Country", [ "País deve ser informado." ]);
        
        if(errors.Count != 0)
            throw new DomainValidationException(errors);
    }
}