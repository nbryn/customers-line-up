namespace CLup.Domain.Shared;

public abstract class Entity
{
    public DateTime CreatedAt { get; protected init; }

    public DateTime UpdatedAt { get; set; }
}
