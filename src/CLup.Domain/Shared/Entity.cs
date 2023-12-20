using System;

namespace CLup.Domain.Shared;

public abstract class Entity<TId>
{
    public TId Id { get; set; }

    public DateTime CreatedAt { get; protected init; }

    public DateTime UpdatedAt { get; set; }
}
