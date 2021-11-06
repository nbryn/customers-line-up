using System;
using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Queries
{
    public class AvailableTimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDto>>>
    {

        public string BusinessId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

