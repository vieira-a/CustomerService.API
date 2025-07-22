namespace Shared.Messaging.Events;

public class CustomerUpdatedEvent
{
    public required Guid CustomerId { get; set; }
    public required string Name { get; set; }
}
