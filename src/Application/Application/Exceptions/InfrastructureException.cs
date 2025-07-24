namespace Application.Exceptions;

public abstract class InfrastructureException(string message) : Exception(message);

public class InternalServerException(string message) : InfrastructureException(message);