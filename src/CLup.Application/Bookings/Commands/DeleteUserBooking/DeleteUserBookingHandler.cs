using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Users;

namespace CLup.Application.Bookings.Commands.DeleteUserBooking;

public sealed class DeleteUserBookingHandler : IRequestHandler<DeleteUserBookingCommand, Result>
{
    private readonly ICLupRepository _repository;

    public DeleteUserBookingHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteUserBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.UserId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFound(user => user?.GetBookingById(command.BookingId), BookingErrors.NotFound)
            .AndThen(booking => booking?.User.RemoveBooking(booking))
            .AddDomainEvent(booking => booking?.DomainEvents.Add(new UserDeletedBookingEvent(booking)))
            .FinallyAsync(_ => _repository.SaveChangesAsync(false, cancellationToken));
}
