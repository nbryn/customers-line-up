using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings
{
    public class BusinessDeleteBookingCommand
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

                return await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.BusinessId)
                        .ToResult()
                        .EnsureDiscard(business => business.OwnerEmail == command.OwnerEmail, (HttpCode.Forbidden, "You don't have access to this business"))
                        .FailureIf(() => _context.Bookings.FirstOrDefaultAsync(x => x.Id == command.BookingId), "Booking not found")
                        .Finally(booking => _context.RemoveAndSave(booking));
            }
        }

    }
}