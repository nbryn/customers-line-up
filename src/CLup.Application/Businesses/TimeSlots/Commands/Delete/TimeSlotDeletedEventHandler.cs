using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.TimeSlots.Commands.Delete
{
    public class TimeSlotDeletedEventHandler : INotificationHandler<DomainEventNotification<TimeSlotDeletedEvent>>
    {
        private readonly ICLupDbContext _context;

        public TimeSlotDeletedEventHandler(ICLupDbContext context) => _context = context;

        public async Task Handle(
            DomainEventNotification<TimeSlotDeletedEvent> @event,
            CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            var users = await _context.Users
                .Include(user => user.Bookings)
                .Where(user => user.Bookings.Any(booking => booking.TimeSlotId == domainEvent.TimeSlot.Id))
                .ToListAsync();

            foreach (var user in users)
            {
                domainEvent.BusinessOwner.BusinessDeletedBookingMessage(domainEvent.TimeSlot.Business, user.Id);
            }

            await _context.UpdateEntity(domainEvent.TimeSlot.Business.Id, domainEvent.TimeSlot.Business);
        }
    }
}