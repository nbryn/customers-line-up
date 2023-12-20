using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings.Events;
using CLup.Application.Shared.Result;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses;

namespace CLup.Application.Bookings.Commands.DeleteBooking;

public class BusinessDeleteBookingHandler : IRequestHandler<BusinessDeleteBookingCommand, Result>
{
    private readonly ICLupRepository _repository;

    public BusinessDeleteBookingHandler(ICLupRepository repository) => _repository = repository;

    public async Task<Result> Handle(BusinessDeleteBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIf(BusinessErrors.NotFound())
            .FailureIf(business => business.GetBooking(BookingId.Create(command.BookingId)), BookingErrors.NotFound())
            .AddDomainEvent(booking => booking.DomainEvents.Add(new BusinessDeletedBookingEvent(booking)))
            .Finally(async booking => await _repository.RemoveAndSave(booking));
}
