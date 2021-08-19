using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;
using CLup.Features.TimeSlots.Commands;

namespace CLup.Features.TimeSlots
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly CLupContext _context;

        public DeleteTimeSlotHandler(CLupContext context) => _context = context;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        {

            return await _context.TimeSlots.FirstOrDefaultAsync(t => t.Id == command.Id)
                    .FailureIf("Time slot not found")
                    .Finally(timeSlot => _context.RemoveAndSave(timeSlot));
        }
    }
}