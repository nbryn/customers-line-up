using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;

namespace CLup.Application.Bookings.Commands.DeleteBooking;

public class BusinessDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<BusinessDeletedBookingEvent>>
{
    private readonly ICLupRepository _repository;

    public BusinessDeletedBookingEventHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DomainEventNotification<BusinessDeletedBookingEvent> @event, CancellationToken cancellationToken)
    {
        var domainEvent = @event.DomainEvent;
        domainEvent.BookingOwner.BookingDeletedMessage(domainEvent.Booking.UserId.Value);

        await _repository.UpdateEntity<Business, BusinessId>(domainEvent.BookingOwner.Id.Value, domainEvent.BookingOwner);
    }
}
