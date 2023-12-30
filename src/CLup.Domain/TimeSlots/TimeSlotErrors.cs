using CLup.Domain.Shared;

namespace CLup.Domain.TimeSlots;

public static class TimeSlotErrors
{
    public static Error NotFound => new("TimeSlots.NotFound", "The time slot was not found.");

    public static Error NoCapacity => new("TimeSlots.NoCapacity", "This time slot is full.");

    public static Error NoAccess => new("TimeSlots.NoAccess", "You cant access this time slot.");

    public static Error TimeSlotsExists => new("TimeSlots.Exists", "Time slots already generated for this date.");
}
