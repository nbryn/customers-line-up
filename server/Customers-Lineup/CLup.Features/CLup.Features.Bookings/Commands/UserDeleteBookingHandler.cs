using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain.Bookings;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings.Commands
{
    public class UserDeleteBookingHandler : IRequestHandler<UserDeleteBookingCommand, Result>
    {
        private readonly CLupContext _context;

        public UserDeleteBookingHandler(CLupContext context) => _context = context;

        public async Task<Result> Handle(UserDeleteBookingCommand command, CancellationToken cancellationToken)
        {
            return await _context.Bookings.Include(b => b.User).Include(b => b.Business).Include(b => b.TimeSlot).FirstOrDefaultAsync(x => x.Id == command.BookingId)
                     .FailureIf("Booking not found.")
                     .AddDomainEvent(booking => booking.AddDomainEvent(new UserDeletedBookingEvent(booking)))
                     .Finally(booking => _context.RemoveAndSave(booking));
        }
    }
}