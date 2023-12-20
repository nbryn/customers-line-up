using System;
using System.Collections.Generic;

namespace CLup.Domain.Shared;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }

    public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

    protected DomainEvent()
    {
    }
}
