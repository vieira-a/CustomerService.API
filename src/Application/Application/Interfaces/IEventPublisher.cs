namespace Application.Interfaces;

public interface IEventPublisher
{
    Task Publish<TEvent>(TEvent eventPub) where TEvent : class;
}