using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Bookings.Commands;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Bookings
{

    public class Handler : IRequestHandler<BusinessDeleteBookingCommand, Result>
    {
        private readonly CLupContext _context;

        public Handler(CLupContext context) => _context = context;

        public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
        {

            return await _context.Businesses.FirstOrDefaultAsync(x => x.Id == command.BusinessId)
                    .ToResult()
                    .EnsureDiscard(business => business.OwnerEmail == command.OwnerEmail, (HttpCode.Forbidden, "You don't have access to this business"))
                    .FailureIf(() => _context.Bookings.FirstOrDefaultAsync(x => x.Id == command.BookingId), "Booking not found")
                    .Finally(booking => _context.RemoveAndSave(booking));
        }
    }
}