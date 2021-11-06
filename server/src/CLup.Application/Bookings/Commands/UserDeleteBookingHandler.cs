using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Data;
using CLup.Domain.Bookings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Bookings.Commands
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