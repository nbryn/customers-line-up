using System;

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
}
