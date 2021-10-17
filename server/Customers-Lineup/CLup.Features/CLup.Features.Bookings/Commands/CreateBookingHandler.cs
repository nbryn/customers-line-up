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

namespace CLup.Features.Bookings.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Result>
    {
        private readonly CLupContext _context;

        public CreateBookingHandler(CLupContext context) => _context = context;

        public async Task<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            return await _context.Bookings.FirstOrDefaultAsync(x =>
                                                           x.TimeSlotId == command.TimeSlotId &&
                                                           x.UserId == command.UserId)
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
                        UserId = command.UserId,
                        TimeSlotId = command.TimeSlotId,
                        BusinessId = business.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    })
                    .Finally(booking => _context.AddAndSave(booking));
        }
    }
}