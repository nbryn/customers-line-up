using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings.Events;
using MediatR;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking
{
    public class UserDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<UserDeletedBookingEvent>>
    {
        private readonly ICLupRepository _repository;

        public UserDeletedBookingEventHandler(ICLupRepository repository) => _repository = repository;

        public async Task Handle(
            DomainEventNotification<UserDeletedBookingEvent> @event,
            CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            domainEvent.User.UserDeletedBookingMessage(domainEvent.Booking, domainEvent.Booking.BusinessId);
            
            await _repository.UpdateEntity(domainEvent.User.Id, domainEvent.User);
        }
    }
}