using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Booking;
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
            var message = domainEvent.Business.BookingDeletedMessage(domainEvent.ReceiverId);
            
            await _context.BusinessMessages.AddAsync(message);
        }
    }
}