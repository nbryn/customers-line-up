using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using FluentValidation;
using MediatR;
using CLup.Domain.Users;
using CLup.Domain.TimeSlots;
using CLup.Application.Shared;
using CLup.Domain.Businesses;

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
            .FailureIfNotFound(business => business.GetTimeSlotById(command.TimeSlotId), TimeSlotErrors.NotFound)
            .Ensure(timeSlot => timeSlot.IsAvailable(), HttpCode.BadRequest, TimeSlotErrors.NoCapacity)
            .AndThen(_ => command.MapToBooking())
            .Validate(_validator)
            .FailureIfNotFoundAsync(
                async booking => (await _repository.FetchUserAggregate(command.UserId))?.CreateBooking(booking),
                UserErrors.NotFound)
            .Ensure(result => result.Success, HttpCode.BadRequest)
            .FinallyAsync(_ => _repository.SaveChangesAsync(cancellationToken));
}
