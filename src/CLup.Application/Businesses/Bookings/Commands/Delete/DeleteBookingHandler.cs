using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{

    public class DeleteBookingHandler : IRequestHandler<BusinessDeleteBookingCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteBookingHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
        {

            return await _context.Businesses.Include(business => business.Owner).FirstOrDefaultAsync(x => x.Id == command.BusinessId)
                    .ToResult()
                    .EnsureDiscard(business => business.Owner.Email == command.OwnerEmail, (HttpCode.Forbidden, "You don't have access to this business"))
                    .FailureIf(() => _context.Bookings.Include(booking => booking.Business).ThenInclude(business => business.Owner).FirstOrDefaultAsync(booking => booking.Id == command.BookingId), "Booking not found")
                    .AddDomainEvent(booking => booking.DomainEvents.Add(new BusinessDeletedBookingEvent(booking.Business.Owner, booking.Business.Id, booking.UserId)))
                    .Finally(booking => _context.RemoveAndSave(booking));
        }
    }
}