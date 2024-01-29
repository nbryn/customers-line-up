using System;
using CLup.Domain.TimeSlots;

namespace CLup.Application.TimeSlots;

public sealed class TimeSlotDto
{
    public Guid Id { get; set; }

    public Guid BusinessId { get; set; }

    public string Business { get; set; }

    public string Date { get; set; }

    public string Start { get; set; }

    public string End { get; set; }

    public string Interval { get; set; }

    public string Capacity { get; set; }

    public bool Available { get; set; }

    public TimeSlotDto FromTimeSlot(TimeSlot timeSlot)
    {
        Id = timeSlot.Id.Value;
        BusinessId = timeSlot.BusinessId.Value;
        Business = timeSlot.BusinessName;
        Date = timeSlot.Start.ToString("dd/MM/yyyy");
        Start = timeSlot.Start.TimeOfDay.ToString().Substring(0, 5);
        Interval = timeSlot.FormatInterval();
        Capacity = $"{timeSlot.Bookings.Count}/{timeSlot.Capacity.ToString()}";
        Available = timeSlot.IsAvailable();

        return this;
    }
}
