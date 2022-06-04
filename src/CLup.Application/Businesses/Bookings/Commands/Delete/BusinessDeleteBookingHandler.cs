using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using MediatR;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{
    public class BusinessDeleteBookingHandler : IRequestHandler<BusinessDeleteBookingCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public BusinessDeleteBookingHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetBusinessBooking(command.BusinessId, command.BookingId),
                    "Business or booking not found.")
                .AddDomainEvent(booking => booking.DomainEvents.Add(new BusinessDeletedBookingEvent(booking)))
                .Finally(booking => _context.RemoveAndSave(booking));
    }
}