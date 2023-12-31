using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using MediatR;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class UserDeleteBookingHandler : IRequestHandler<UserDeleteBookingCommand, Result>
{
    private readonly ICLupRepository _repository;

    public UserDeleteBookingHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UserDeleteBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.UserId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFound(user => user.GetBookingById(BookingId.Create(command.BookingId)), BookingErrors.NotFound)
            .AddDomainEvent(booking => booking.DomainEvents.Add(new UserDeletedBookingEvent(booking)))
            .Finally(async booking => await _repository.RemoveAndSave(booking));
}
