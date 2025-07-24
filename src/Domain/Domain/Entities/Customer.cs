using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Entities;

public sealed class Customer : Entity
{
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Name { get; private set; }

    public string Email { get; private set; }

    private readonly List<Address> _addresses = [ ];

    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    private Customer(string name, string email)
    {
        Name = name;
        Email = email;
        Validate();
    }

    private Customer(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
        Validate();
    }

    public static Customer Create(string name, string email)
    {
        return new Customer(name, email);
    }

    public static Customer? Restore(Guid id, string name, string email)
    {
        return new Customer(id, name, email);
    }

    public void UpdateName(string newName)
    {
        Name = newName;
        Validate();
    }

    public void AddAddress(Address address)
    {
        _addresses.Add(address);
    }

    private void Validate()
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add("Name", [ "Nome não pode ser vazio." ]);

        if (Name.Length < 2)
            errors.Add("Name", [ "Nome deve ter pelo menos 2 caracteres." ]);

        if (string.IsNullOrWhiteSpace(Email))
            errors.Add("Email", [ "Email não pode ser vazio." ]);

        if (!EmailRegex.IsMatch(Email))
            errors.Add("Email", [ "Email em formato inválido." ]);

        if (errors.Count != 0)
            throw new DomainValidationException(errors);
    }
}