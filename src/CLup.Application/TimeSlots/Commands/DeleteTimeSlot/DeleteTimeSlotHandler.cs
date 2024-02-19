using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using MediatR;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.Events;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
{
    private readonly ICLupRepository _repository;

    public DeleteTimeSlotHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.OwnerId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business?.GetTimeSlotById(command.TimeSlotId), TimeSlotErrors.NotFound)
            .AndThen(timeSlot => timeSlot?.Business?.RemoveTimeSlot(timeSlot))
            .AddDomainEvent(timeSlot => timeSlot?.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot)))
            .FinallyAsync(_ => _repository.SaveChangesAsync(false, cancellationToken));
}
