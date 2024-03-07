using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Shared;
using MediatR;

namespace CLup.Infrastructure.Services;

public sealed class DomainEventService : IDomainEventService
{
    private readonly IMediator _mediator;

    public DomainEventService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Publish(DomainEvent domainEvent)
        => await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        => (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
}
