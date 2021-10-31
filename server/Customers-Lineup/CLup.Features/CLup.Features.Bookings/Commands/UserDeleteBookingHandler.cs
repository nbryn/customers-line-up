using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
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
            return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == command.BookingId)
                     .FailureIf("Booking not found.")
                     .Finally(booking => _context.RemoveAndSave(booking));
        }
    }
}