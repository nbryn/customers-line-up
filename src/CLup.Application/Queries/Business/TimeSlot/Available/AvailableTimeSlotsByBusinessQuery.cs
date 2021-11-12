using System;
using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.Business.TimeSlot.Available
{
    public class AvailableTimeSlotsByBusinessQuery : IRequest<Result<List<TimeSlotDto>>>
    {

        public string BusinessId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

