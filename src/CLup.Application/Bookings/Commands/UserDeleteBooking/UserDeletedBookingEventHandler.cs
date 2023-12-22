using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class UserDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<UserDeletedBookingEvent>>
{
    private readonly ICLupRepository _repository;

    public UserDeletedBookingEventHandler(ICLupRepository repository) => _repository = repository;

    public async Task Handle(
        DomainEventNotification<UserDeletedBookingEvent> @event,
        CancellationToken cancellationToken)
    {
        var domainEvent = @event.DomainEvent;
        domainEvent.Owner.BookingDeletedMessage(domainEvent.Booking);

        await _repository.UpdateEntity(domainEvent.Booking.User.Id.Value, domainEvent.Booking.User);
    }
}
