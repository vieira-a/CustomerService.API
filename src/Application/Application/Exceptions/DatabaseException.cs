namespace Application.Exceptions;

public class DatabaseException(string message) : InfrastructureException(message);