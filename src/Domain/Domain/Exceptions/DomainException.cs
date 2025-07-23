namespace Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) {}
}

public class DomainValidationException : DomainException
{
    public IReadOnlyDictionary<string, List<string>> ValidationErrors { get; }

    public DomainValidationException(IReadOnlyDictionary<string, List<string>> validationErrors) : base(
        "Erro de validação de domínio.")
    {
        ValidationErrors = validationErrors;
    }
}