using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Bookings;
using MediatR;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{
    public class BusinessDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<BusinessDeletedBookingEvent>>
    {
        private readonly ICLupRepository _repository;

        public BusinessDeletedBookingEventHandler(ICLupRepository repository) => _repository = repository;

        public async Task Handle(DomainEventNotification<BusinessDeletedBookingEvent> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            domainEvent.Booking.User.BusinessDeletedBookingMessage(domainEvent.Business, domainEvent.Booking.UserId);
            
            await _repository.UpdateEntity(domainEvent.Business.Id, domainEvent.Business);
        }
    }
}