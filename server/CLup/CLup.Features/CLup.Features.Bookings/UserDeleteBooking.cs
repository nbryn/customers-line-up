using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Bookings
{
    public class UserDeleteBooking
    {
        public class Command : IRequest<Result>
        {
            public string BookingId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;

            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == command.BookingId);

                if (booking == null)
                {
                    return Result.NotFound("Booking not found");
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }

    }
}