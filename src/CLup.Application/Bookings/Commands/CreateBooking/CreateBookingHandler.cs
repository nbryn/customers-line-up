using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using FluentValidation;
using MediatR;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using CLup.Application.Shared.Result;
using CLup.Domain.TimeSlots;
using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Result>
{
    private readonly IValidator<Booking> _validator;
    private readonly ICLupRepository _repository;
    private readonly IMapper _mapper;

    public CreateBookingHandler(
        IValidator<Booking> validator,
        ICLupRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(UserId.Create(command.UserId))
            .FailureIf(UserErrors.NotFound(UserId.Create(command.UserId)))
            .Ensure(user => !user.BookingExists(TimeSlotId.Create(command.TimeSlotId)), HttpCode.BadRequest,
                UserErrors.BookingExists())
            .FailureIf(async _ => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId)),
                BusinessErrors.NotFound())
            .FailureIf(business => business.GetTimeSlotById(TimeSlotId.Create(command.TimeSlotId)),
                TimeSlotErrors.NotFound())
            .Ensure(timeSlot => timeSlot.IsAvailable(), HttpCode.BadRequest, TimeSlotErrors.NoCapacity())
            .AndThen(_ => _mapper.Map<Booking>(command))
            .Validate(_validator)
            .Finally(async booking => await _repository.AddAndSave(booking));
}
