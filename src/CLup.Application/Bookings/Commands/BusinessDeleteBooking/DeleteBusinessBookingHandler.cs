using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Businesses;
using MediatR;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class DeleteBusinessBookingHandler : IRequestHandler<DeleteBusinessBookingCommand, Result>
{
    private readonly ICLupRepository _repository;

    public DeleteBusinessBookingHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteBusinessBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.OwnerId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business?.GetBookingById(command.BookingId), BookingErrors.NotFound)
            .AndThen(booking => booking?.Business.RemoveBooking(booking))
            .AddDomainEvent(booking => booking?.DomainEvents.Add(new BusinessDeletedBookingEvent(booking)))
            .FinallyAsync(_ => _repository.SaveChangesAsync(false, cancellationToken));
}
