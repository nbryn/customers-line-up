using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;

namespace CLup.Features.TimeSlots.EventHandlers
{
    public class TimeSlotDeletedEventHandler : INotificationHandler<TimeSlotDeletedEvent>
    {
        private readonly CLupContext _context;

        public TimeSlotDeletedEventHandler(CLupContext context) => _context = context;

        public async Task Handle(TimeSlotDeletedEvent timeSlotDeletedEvent, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Include(u => u.Bookings)
                .Where(u => u.Bookings.Any(b => b.TimeSlotId == timeSlotDeletedEvent.TimeSlot.Id))
                .ToListAsync();

            var messages = users.Select(u => MessageFactory.BookingDeletedMessage(timeSlotDeletedEvent.TimeSlot.Business, u.Id));

            await _context.AddRangeAsync(messages);
        }
    }
}