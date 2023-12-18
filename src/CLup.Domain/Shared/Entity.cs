using System;

namespace CLup.Domain.Shared;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    public DateTime CreatedAt { get; protected init; }

    public DateTime UpdatedAt { get; set; }
}