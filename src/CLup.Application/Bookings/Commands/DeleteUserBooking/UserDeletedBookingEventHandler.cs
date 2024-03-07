using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings.Events;

namespace CLup.Application.Bookings.Commands.DeleteUserBooking;

public sealed class UserDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<UserDeletedBookingEvent>>
{
    private readonly ICLupRepository _repository;

    public UserDeletedBookingEventHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DomainEventNotification<UserDeletedBookingEvent> @event,
        CancellationToken cancellationToken)
    {
        var domainEvent = @event.DomainEvent;
        domainEvent.Owner.BookingDeletedMessage(domainEvent.Booking);

        await _repository.SaveChangesAsync(true, cancellationToken);
    }
}
