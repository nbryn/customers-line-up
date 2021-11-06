using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Queries
{
    public class TimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDto>>>
    {

        public string BusinessId { get; set; }
    }
}

