using Application.Interfaces;
using MassTransit;

namespace Infrastructure.Messaging.RabbitMQ;

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public Task Publish<TEvent>(TEvent eventPub) where TEvent : class
    {
        Console.WriteLine($"Publishing event: {eventPub}");
        return _publishEndpoint.Publish(eventPub);
    }
}