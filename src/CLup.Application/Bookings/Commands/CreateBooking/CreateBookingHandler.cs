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
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business.GetTimeSlotById(TimeSlotId.Create(command.TimeSlotId)),
                TimeSlotErrors.NotFound)
            .AndThen(_ => _mapper.Map<Booking>(command))
            .Validate(_validator)
            .FailureIfNotFoundAsync(
                async booking => (await _repository.FetchUserAggregate(command.UserId))?.AddBooking(booking),
                UserErrors.NotFound)
            .Ensure(result => result.Success, HttpCode.BadRequest)
            .FinallyAsync(_ => _repository.SaveChangesAsync(cancellationToken));
}
