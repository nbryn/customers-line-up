using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.EventHandlers.TimeSlot
{
    public class TimeSlotDeletedEventHandler : INotificationHandler<DomainEventNotification<TimeSlotDeletedEvent>>
    {
        private readonly ICLupDbContext _context;

        public TimeSlotDeletedEventHandler(ICLupDbContext context) => _context = context;

        public async Task Handle(DomainEventNotification<TimeSlotDeletedEvent> @event, CancellationToken cancellationToken)
        {
            var domainEvent = @event.DomainEvent;
            var users = await _context.Users
                .Include(u => u.Bookings)
                .Where(u => u.Bookings.Any(b => b.TimeSlotId == domainEvent.TimeSlot.Id))
                .ToListAsync();

            var messages = users.Select(u => domainEvent.TimeSlot.Business.BookingDeletedMessage(u.Id));

            await _context.Messages.AddRangeAsync(messages);
        }
    }
}