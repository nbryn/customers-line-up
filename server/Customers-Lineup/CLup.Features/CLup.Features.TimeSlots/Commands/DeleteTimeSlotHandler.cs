using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.TimeSlots.Commands
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly CLupContext _context;

        public DeleteTimeSlotHandler(CLupContext context) => _context = context;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        {
            return await _context.TimeSlots.Include(t => t.Business).FirstOrDefaultAsync(t => t.Id == command.Id)
                    .FailureIf("Time slot not found")
                    // Check if TimeSlot has bookings -> Alert before deleting?
                    .AddDomainEvent(timeSlot => timeSlot.AddDomainEvent(new TimeSlotDeletedEvent(timeSlot)))
                    .Finally(timeSlot => _context.RemoveAndSave(timeSlot));
        }
    }
}