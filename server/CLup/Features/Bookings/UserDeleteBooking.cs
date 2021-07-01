using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Util;

namespace CLup.Bookings
{
    public class UserDeleteBooking
    {
        public class Command : IRequest<Result>
        {
            public string UserEmail { get; set; }
            public string TimeSlotId { get; set; }

            public Command(string userEmail, string timeSlotId)
            {
                UserEmail = userEmail;
                TimeSlotId = timeSlotId;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;

            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.TimeSlotId == command.TimeSlotId &&
                                                                          x.UserEmail == command.UserEmail);

                if (booking == null)
                {
                    return Result.NotFound("Booking not found");
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                return Result.Deleted();
            }
        }

    }
}