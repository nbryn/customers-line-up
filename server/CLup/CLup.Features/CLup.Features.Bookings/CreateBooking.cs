using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;

namespace CLup.Features.Bookings
{
    public class CreateBooking
    {
        public class Command : IRequest<Result>
        {
            public string UserEmail { get; set; }
            public string TimeSlotId { get; set; }

            public Command(string userEmail, string timeSlotId)
            {
                UserEmail = userEmail;
                TimeSlotId = timeSlotId;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;

            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var bookingExists = await _context.Bookings.FirstOrDefaultAsync(x =>
                                                               x.TimeSlotId == command.TimeSlotId &&
                                                               x.UserEmail == command.UserEmail);
                if (bookingExists != null)
                {
                    return Result.Conflict("You already have a booking for this time slot.");
                }

                var timeSlot = await _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .FirstOrDefaultAsync(x => x.Id == command.TimeSlotId);

                if (timeSlot == null)
                {
                    return Result.NotFound("Time Slot does not exists.");
                }

                if (timeSlot.Bookings.Count() >= timeSlot.Capacity)
                {
                    return Result.Conflict("This time slot is full.");
                }

                var business = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == timeSlot.BusinessId);

                if (business == null)
                {
                    //Handle business does not exists - Gather null checks in one place?
                }

                var booking = new Booking
                {
                    Id = Guid.NewGuid().ToString(),
                    UserEmail = command.UserEmail,
                    TimeSlotId = command.TimeSlotId,
                    BusinessId = business.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
        }

    }
}