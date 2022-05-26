using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Bookings.Commands.Delete
{
    public class UserDeleteBookingHandler : IRequestHandler<UserDeleteBookingCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public UserDeleteBookingHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(UserDeleteBookingCommand command, CancellationToken cancellationToken)
            => await _context.Bookings
                .Include(booking => booking.User)
                .Include(booking => booking.Business).Include(b => b.TimeSlot)
                .FirstOrDefaultAsync(x => x.Id == command.BookingId)
                .FailureIf("Booking not found.")
                .AddDomainEvent(booking => booking.DomainEvents.Add(new UserDeletedBookingEvent(booking)))
                .Finally(booking => _context.RemoveAndSave(booking));
        
    }
}