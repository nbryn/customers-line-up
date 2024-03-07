using CLup.Domain.TimeSlots;

namespace CLup.Application.TimeSlots;

public sealed class TimeSlotDto
{
    public Guid Id { get; init; }

    public Guid BusinessId { get; init; }

    public string Business { get; init; }

    public string Date { get; init; }

    public string Start { get; init; }

    public string End { get; init; }

    public string Interval { get; init; }

    public string Capacity { get; init; }

    public bool Available { get; init; }

    public static TimeSlotDto FromTimeSlot(TimeSlot timeSlot)
    {
        return new TimeSlotDto()
        {
            Id = timeSlot.Id.Value,
            BusinessId = timeSlot.BusinessId.Value,
            Business = timeSlot.BusinessName,
            Date = timeSlot.Date.ToString("dd/MM/yyyy"),
            Start = timeSlot.TimeInterval.Start.ToString(),
            End = timeSlot.TimeInterval.End.ToString(),
            Interval = timeSlot.FormatInterval(),
            Capacity = $"{timeSlot.Bookings.Count}/{timeSlot.Capacity.ToString()}",
            Available = timeSlot.IsAvailable().Success,
        };
    }
}
