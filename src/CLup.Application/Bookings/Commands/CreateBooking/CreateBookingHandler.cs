using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;
using FluentValidation;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Result>
{
    private readonly IValidator<Booking> _validator;
    private readonly ICLupRepository _repository;

    public CreateBookingHandler(IValidator<Booking> validator, ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessById(command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business?.GetTimeSlotById(command.TimeSlotId), TimeSlotErrors.NotFound)
            .FlatMap(timeSlot => timeSlot.IsAvailable(), HttpCode.BadRequest)
            .AndThen(_ => command.MapToBooking())
            .Validate(_validator)
            .FailureIfNotFoundAsync(
                async booking => (await _repository.FetchUserAggregate(command.UserId))?.CreateBooking(booking),
                UserErrors.NotFound)
            .FlatMap()
            .FinallyAsync(() => _repository.SaveChangesAsync(true, cancellationToken));
}
