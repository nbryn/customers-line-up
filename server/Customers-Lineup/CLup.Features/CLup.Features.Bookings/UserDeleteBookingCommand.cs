using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings
{
    public class UserDeleteBookingCommand
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
                return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == command.BookingId)
                         .FailureIf("Booking not found.")
                         .Finally(booking => _context.RemoveAndSave(booking));
            }
        }
    }
}