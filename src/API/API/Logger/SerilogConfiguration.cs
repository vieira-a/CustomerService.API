using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace API.Logger;

public static class SerilogConfiguration
{
    public static void ConfigureLogging(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console(new CompactJsonFormatter())
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}