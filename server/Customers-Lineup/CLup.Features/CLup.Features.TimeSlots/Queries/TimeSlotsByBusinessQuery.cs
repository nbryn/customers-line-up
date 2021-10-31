using System.Collections.Generic;

using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.TimeSlots.Queries
{
    public class TimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDTO>>>
    {

        public string BusinessId { get; set; }
    }
}

