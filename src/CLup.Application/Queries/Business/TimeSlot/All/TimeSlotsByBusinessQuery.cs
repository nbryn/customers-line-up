using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.Business.TimeSlot.All
{
    public class TimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDto>>>
    {

        public string BusinessId { get; set; }
    }
}

