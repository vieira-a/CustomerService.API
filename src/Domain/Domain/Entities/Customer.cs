namespace Domain.Entities;

public sealed class Customer : Entity
{
    public string Name { get; private set; }
    
    public string Email { get; private set; }
    
    private readonly List<Address> _addresses = [];
    
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    
    private Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }
    
    private Customer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public static Customer Create(string name, string email)
    {
       return new Customer(name, email);
    }

    public static Customer? Restore(Guid id, string name, string email)
    {
        return new Customer(id, name, email);
    }
    
    public void AddAddress(Address address)
    {
        _addresses.Add(address);
    }
}