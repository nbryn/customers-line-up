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
using CLup.Domain.TimeSlots;
using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;

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
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIf(BusinessErrors.NotFound)
            .FailureIf(business => business.GetTimeSlotById(TimeSlotId.Create(command.TimeSlotId)),
                TimeSlotErrors.NotFound)
            .AndThen(_ => _mapper.Map<Booking>(command))
            .Validate(_validator)
            // TODO: This doesn't fail if user doesn't exits
            .FailureIf(booking => GetUser(command.UserId, booking), UserErrors.NotFound)
            .Ensure(entry => entry.user.AddBooking(entry.booking).Success, HttpCode.BadRequest)
            .Finally(_ => _repository.SaveChangesAsync(true, cancellationToken));

    private async Task<(User user, Booking booking)> GetUser(UserId userId, Booking booking)
    {
        var user = await _repository.FetchUserAggregate(userId);
        return (user, booking);
    }
}
