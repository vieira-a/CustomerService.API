namespace Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime Created { get; private set; }
    public DateTime Modified { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        Created = DateTime.UtcNow;
        Modified = DateTime.UtcNow;
    }
}