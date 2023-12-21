using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using MediatR;
using CLup.Application.Shared.Result;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.Events;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
{
    private readonly ICLupRepository _repository;

    public DeleteTimeSlotHandler(ICLupRepository repository) => _repository = repository;

    public async Task<Result> Handle(DeleteTimeSlotCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(BusinessId.Create(command.BusinessId))
            .FailureIf(BusinessErrors.NotFound())
            .Ensure(business => business?.OwnerId.Value == command.OwnerId, HttpCode.Forbidden, TimeSlotErrors.NoAccess())
            .FailureIf(business => business?.GetTimeSlotById(TimeSlotId.Create(command.TimeSlotId)), TimeSlotErrors.NotFound())
            // Check if TimeSlot has bookings -> Alert before deleting?
            .AddDomainEvent(timeSlot => timeSlot?.DomainEvents.Add(new TimeSlotDeletedEvent(timeSlot)))
            .Finally(timeSlot => _repository.RemoveAndSave(timeSlot));
}
