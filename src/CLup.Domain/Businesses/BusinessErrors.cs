using CLup.Domain.Shared;

namespace CLup.Domain.Businesses;

public static class BusinessErrors
{
    public static readonly Error NotFound = new("Business.NotFound", "The business was not found.");

    public static readonly Error TimeSlotLengthExceedsOpeningHours = new("Business.TImeSlot.ExceedsOpeningHours", "Time slot length can't exceed opening hours.");

    public static readonly Error InvalidTimeSlotLength = new("Business.TimeSlot.InvalidTimeSlotLength", "Time slot length must be divisible by 5.");
}
