using CLup.Domain.Shared;

namespace CLup.Application.Shared.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
