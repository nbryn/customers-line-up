using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.TimeSlot.Delete
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteTimeSlotHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        {
            return await _context.TimeSlots.Include(t => t.Business).FirstOrDefaultAsync(t => t.Id == command.Id)
                    .FailureIf("Time slot not found")
                    // Check if TimeSlot has bookings -> Alert before deleting?
                    .AddDomainEvent(timeSlot => timeSlot.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot)))
                    .Finally(timeSlot => _context.RemoveAndSave(timeSlot));
        }
    }
}