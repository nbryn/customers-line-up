using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.TimeSlots.Commands.Delete
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteTimeSlotHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        {
            return await _context.TimeSlots
            .Include(timeSlot => timeSlot.Business)
            .ThenInclude(business => business.Owner)
            .FirstOrDefaultAsync(t => t.Id == command.Id)
                    .FailureIf("Time slot not found")
                    // Check if TimeSlot has bookings -> Alert before deleting?
                    .AddDomainEvent(timeSlot => timeSlot.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot.Business.Owner, timeSlot)))
                    .Finally(timeSlot => _context.RemoveAndSave(timeSlot));
        }
    }
}