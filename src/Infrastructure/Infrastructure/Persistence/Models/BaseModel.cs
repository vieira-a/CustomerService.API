namespace Infrastructure.Persistence.Models;

public abstract class BaseModel
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    protected BaseModel()
    {
        Created = DateTime.UtcNow;
        Modified = DateTime.UtcNow;
    }
}