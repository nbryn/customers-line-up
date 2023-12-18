using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings.Events;
using MediatR;

namespace CLup.Application.Bookings.Commands.DeleteBooking
{
    public class BusinessDeleteBookingHandler : IRequestHandler<BusinessDeleteBookingCommand, Result>
    {
        private readonly ICLupRepository _repository;

        public BusinessDeleteBookingHandler(ICLupRepository repository) => _repository = repository;

        public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetBusinessBooking(command.BusinessId, command.BookingId),
                    "Business or booking not found.")
                .AddDomainEvent(booking => booking.DomainEvents.Add(new BusinessDeletedBookingEvent(booking)))
                .Finally(booking => _repository.RemoveAndSave(booking));
    }
}