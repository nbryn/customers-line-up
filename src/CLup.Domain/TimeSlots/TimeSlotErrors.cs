using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Domain.TimeSlots;

public static class TimeSlotErrors
{
    public static Error NotFound(TimeSlotId timeSlotId) =>
        new("TimeSlots.NotFound", $"The time slot with the id {timeSlotId.Value} was not found.");

    public static Error NoCapacity() =>
        new("TimeSlots.NoCapacity", "This time slot is full.");
}
