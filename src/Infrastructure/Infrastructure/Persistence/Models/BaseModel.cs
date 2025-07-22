namespace Infrastructure.Persistence.Models;

public abstract class BaseModel
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}