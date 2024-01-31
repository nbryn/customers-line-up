using CLup.Domain.Shared;

namespace CLup.Domain.TimeSlots;

public static class TimeSlotErrors
{
    public static readonly Error NotFound = new("TimeSlots.NotFound", "The time slot was not found.");

    public static readonly Error NoCapacity = new("TimeSlots.NoCapacity", "This time slot is full.");

    public static readonly Error TimeSlotsExists = new("TimeSlots.Exists", "Time slots already generated for this date.");
}
