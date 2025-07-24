using System.Reflection;
using Microsoft.OpenApi.Models;

namespace API.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CustomerService.API",
                Version = "v1",
                Description = "Customer Service API Documentation",
                Contact = new OpenApiContact
                {
                    Name = "Anderson Vieira",
                    Email = "asvieira.dev@gmail.com"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    public static WebApplication UseSwaggerDocumentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerService.API");
            options.RoutePrefix = "api/docs";
        });

        return app;
    }
}