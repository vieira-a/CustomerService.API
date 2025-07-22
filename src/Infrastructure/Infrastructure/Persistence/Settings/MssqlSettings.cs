namespace Infrastructure.Persistence.Settings;

public class MssqlSettings
{
    public string Host { get; init; } = null!;
    public int Port { get; init; }
    public string Database { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    
}