using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using MediatR;

namespace CLup.Application.Users.Bookings.Commands.Delete
{
    public class UserDeleteBookingHandler : IRequestHandler<UserDeleteBookingCommand, Result>
    {
        private readonly ICLupRepository _repository;

        public UserDeleteBookingHandler(ICLupRepository repository) => _repository = repository;

        public async Task<Result> Handle(UserDeleteBookingCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.UserEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetBooking(command.BookingId), "Booking not found.")
                .AddDomainEvent(booking => booking.DomainEvents.Add(new UserDeletedBookingEvent(booking)))
                .Finally(booking => _repository.RemoveAndSave(booking));
        
    }
}