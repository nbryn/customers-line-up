using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Bookings;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Bookings.Commands.Create
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Result>
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
            => await _repository.FetchUserAggregate(command.UserId)
                .FailureIf("User not found.")
                .Ensure(user => !user.BookingExists(command.TimeSlotId),
                    "You already have a booking for this time slot.")
                .FailureIf(async _ => await _repository.FetchTimeSlot(command.TimeSlotId), "Time Slot does not exist.")
                .Ensure(timeSlot => timeSlot.Bookings.Count() < timeSlot.Capacity, "This time slot is full.")
                .AndThen(_ => _mapper.Map<Booking>(command))
                .Validate(_validator)
                .Finally(booking => _repository.AddAndSave(booking));
    }
}