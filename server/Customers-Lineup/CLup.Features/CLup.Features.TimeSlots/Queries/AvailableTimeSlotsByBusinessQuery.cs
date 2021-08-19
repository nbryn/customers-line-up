using System;
using System.Collections.Generic;

using MediatR;

using CLup.Features.Common;

namespace CLup.Features.TimeSlots.Queries
{
    public class AvailableTimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDTO>>>
    {

        public string BusinessId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

