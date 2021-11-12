using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Booking;
using CLup.Domain.Message;
using MediatR;

namespace CLup.Application.EventHandlers.Booking
{
    public class BusinessDeletedBookingEventHandler : INotificationHandler<DomainEventNotification<BusinessDeletedBookingEvent>>
    {
        private readonly ICLupDbContext _context;

        public BusinessDeletedBookingEventHandler(ICLupDbContext context) => _context = context;

        public async Task Handle(DomainEventNotification<BusinessDeletedBookingEvent> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            var message = domainEvent.Booking.Business.BookingDeletedMessage(domainEvent.Booking.UserId);
            
            await _context.BusinessMessages.AddAsync(message);
        }
    }
}