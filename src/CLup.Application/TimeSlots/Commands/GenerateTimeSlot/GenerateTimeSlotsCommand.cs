using System;
using CLup.Application.Shared.Result;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public DateTime Start { get; set; }
}
