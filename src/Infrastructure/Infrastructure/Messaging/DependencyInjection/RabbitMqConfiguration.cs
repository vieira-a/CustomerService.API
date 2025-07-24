using System.Reflection;
using Application.Interfaces;
using Infrastructure.Messaging.RabbitMQ;
using Infrastructure.Messaging.RabbitMQ.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging.DependencyInjection;

public static class RabbitMqConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IEventPublisher, RabbitMqEventPublisher>();
        return services;
    }
}