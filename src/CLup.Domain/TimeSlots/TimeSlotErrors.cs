using CLup.Domain.Shared;

namespace CLup.Domain.TimeSlots;

public static class TimeSlotErrors
{
    public static readonly Error NotFound = new("TimeSlot.NotFound", "The time slot was not found.");

    public static readonly Error NoCapacity = new("TimeSlot.NoCapacity", "This time slot is full.");

    public static readonly Error Exists = new("TimeSlot.Exists", "Time slots already generated for this date.");

    public static readonly Error InThePast = new("TimeSlot.IsInThePast", "Time slot not available.");

    public static readonly Error TimeSlotCannotBeGeneratedOnDateInThePast =
        new("TimeSlot.CannotBeGeneratedOnDateInThePast", "Time slots can't be on a date in the past.");
}
