using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Bookings
{
    public class BusinessDeleteBooking
    {
        public class Command : IRequest<Result>
        {
            public string OwnerEmail { get; set; }
            public string BookingId { get; set; }
            public string BusinessId { get; set; }


            public Command(
                string ownerEmail,
                string bookingId,
                string businessId
                )
            {
                OwnerEmail = ownerEmail;
                BookingId = bookingId;
                BusinessId = businessId;

            }
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

                var business = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.BusinessId);

                if (business.OwnerEmail != command.OwnerEmail)
                {
                    return Result.Forbidden("You don't have access to this business");
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }

    }
}