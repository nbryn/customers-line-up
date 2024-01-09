using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.Events;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using MediatR;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class BusinessDeleteBookingHandler : IRequestHandler<BusinessDeleteBookingCommand, Result>
{
    private readonly ICLupRepository _repository;

    public BusinessDeleteBookingHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIfNotFound(BusinessErrors.NotFound)
            .Ensure(business => business.OwnerId.Value == command.OwnerId.Value, HttpCode.Forbidden, BusinessErrors.InvalidOwner)
            .FailureIfNotFound(business => business.GetBookingById(BookingId.Create(command.BookingId)), BookingErrors.NotFound)
            .AddDomainEvent(booking => booking.DomainEvents.Add(new BusinessDeletedBookingEvent(booking)))
            .FinallyAsync(booking => _repository.RemoveAndSave(booking));
}
