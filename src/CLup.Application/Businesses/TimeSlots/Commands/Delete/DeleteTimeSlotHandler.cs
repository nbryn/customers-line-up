using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands.Delete
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public DeleteTimeSlotHandler(ICLupDbContext context) => _context = context;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetTimeSlot(command.TimeSlotId), "Time slot or business not found")
                // Check if TimeSlot has bookings -> Alert before deleting?
                .AddDomainEvent(timeSlot => timeSlot.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot)))
                .Finally(timeSlot => _context.RemoveAndSave(timeSlot));
    }
}