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
        private readonly ICLupDbContext _context;

        public BusinessDeletedBookingEventHandler(ICLupDbContext context) => _context = context;

        public async Task Handle(DomainEventNotification<BusinessDeletedBookingEvent> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            domainEvent.Booking.User.BusinessDeletedBookingMessage(domainEvent.Business, domainEvent.Booking.UserId);
            
            await _context.UpdateEntity(domainEvent.Business.Id, domainEvent.Business);
        }
    }
}