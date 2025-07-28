namespace Shared.Messaging.Events;

public class CustomerDeletedEvent
{
    public required Guid CustomerId { get; init; }
}