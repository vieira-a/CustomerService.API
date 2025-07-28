using Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.RabbitMQ;

public class RabbitMqEventPublisher(ILogger<RabbitMqEventPublisher> logger, IPublishEndpoint publishEndpoint)
    : IEventPublisher
{

    public Task Publish<TEvent>(TEvent eventPub) where TEvent : class
    {
        logger.LogInformation("Publicando evento {@Event}", eventPub);
        return publishEndpoint.Publish(eventPub);
    }
}