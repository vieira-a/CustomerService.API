namespace Domain.Exceptions;

public sealed class DomainValidationException(IReadOnlyList<string> errors)
    : DomainException("Erro de validação de domínio.")
{
    public IReadOnlyList<string> Errors { get; } = errors.ToList();
}