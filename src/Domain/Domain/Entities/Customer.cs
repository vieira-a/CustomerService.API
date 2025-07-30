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

        private const string EmptyNameExceptionMessage = "O nome do cliente é obrigatório.";
        private const string InvalidNameExceptionMessage = "O nome do cliente deve conter pelo menos dois dígitos.";
        private const string EmptyEmailExceptionMessage = "O e-mail do cliente é obrigatório.";
        private const string InvalidEmailExceptionMessage = "O e-mail do cliente está com formato inválido.";
        private const string AddressConflictExceptionMessage = "Este CEP já foi cadastrado.";

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

        public void AddAddress(Address newAddress)
        {
            var errors = new List<string>();

            var normalizedZipCode = newAddress.ZipCode.Trim();
            
            var exists = _addresses.Any(a => a.ZipCode.Trim() == normalizedZipCode);

            if (exists)
            {
                errors.Add(AddressConflictExceptionMessage);
                throw new DomainValidationException(errors);
            }
            
            _addresses.Add(newAddress);
        }
        
        public void RestoreAddress(Address address)
        {
            _addresses.Add(address); 
        }

        private void Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(Name)) errors.Add(EmptyNameExceptionMessage);

            if (!string.IsNullOrEmpty(Name) && Name.Length < 2) errors.Add(InvalidNameExceptionMessage);

            if (string.IsNullOrWhiteSpace(Email)) errors.Add(EmptyEmailExceptionMessage);

            else if (!EmailRegex.IsMatch(Email)) errors.Add(InvalidEmailExceptionMessage);

            if (errors.Count != 0) throw new DomainValidationException(errors);
        }
    }