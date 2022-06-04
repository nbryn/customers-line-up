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
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Bookings.Commands.Create
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Result>
    {
        private readonly IValidator<Booking> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookingHandler(
            IValidator<Booking> validator,
            ICLupDbContext context,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.UserId)
                .FailureIf("User not found.")
                .EnsureDiscard(user => !user.BookingExists(command.TimeSlotId),
                    "You already have a booking for this time slot.")
                .FailureIf(() => _context.TimeSlots
                        .Include(timeSlot => timeSlot.Bookings)
                        .FirstOrDefaultAsync(timeSlot => timeSlot.Id == command.TimeSlotId),
                    "Time Slot does not exist.")
                .EnsureDiscard(timeSlot => timeSlot.Bookings.Count() < timeSlot.Capacity, "This time slot is full.")
                .AndThen(() => _mapper.Map<Booking>(command))
                .Validate(_validator)
                .Finally(booking => _context.AddAndSave(booking));
    }
}