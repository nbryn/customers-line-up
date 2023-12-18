using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses.TimeSlots;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot
{
    public class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly ICLupRepository _repository;

        public DeleteTimeSlotHandler(ICLupRepository repository) => _repository = repository;

        public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .FailureIf(user => user.GetTimeSlot(command.TimeSlotId), "Time slot or business not found")
                // Check if TimeSlot has bookings -> Alert before deleting?
                .AddDomainEvent(timeSlot => timeSlot.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot)))
                .Finally(timeSlot => _repository.RemoveAndSave(timeSlot));
    }
}