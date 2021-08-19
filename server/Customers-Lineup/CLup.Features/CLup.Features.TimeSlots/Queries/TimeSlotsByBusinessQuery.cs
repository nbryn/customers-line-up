using System.Collections.Generic;

using MediatR;

using CLup.Features.Common;

namespace CLup.Features.TimeSlots.Queries
{
    public class TimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDTO>>>
    {

        public string BusinessId { get; set; }
    }
}

