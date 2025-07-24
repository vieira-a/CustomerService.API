namespace Shared.Messaging.Events;

public class CustomerCreatedEvent
{
    public required Guid CustomerId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}