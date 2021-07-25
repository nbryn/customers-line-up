using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Extensions;

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
                return await _context.Bookings.FirstOrDefaultAsync(x =>
                                                               x.TimeSlotId == command.TimeSlotId &&
                                                               x.UserEmail == command.UserEmail)
                        .ToResult()
                        .EnsureDiscard(booking => booking == null, "You already have a booking for this time slot.")
                        .FailureIf(() => _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .FirstOrDefaultAsync(x => x.Id == command.TimeSlotId), "Time Slot does not exists.")

                        .Ensure(timeSlot => timeSlot.Bookings.Count() < timeSlot.Capacity, "This time slot is full.")
                        .AndThen(timeSlot => _context.Businesses.FirstOrDefaultAsync(x => x.Id == timeSlot.BusinessId))
                        .AndThen(business => new Booking
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserEmail = command.UserEmail,
                            TimeSlotId = command.TimeSlotId,
                            BusinessId = business.Id,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                        })
                        .Execute(booking => _context.AddAndSave(booking));
            }
        }

    }
}