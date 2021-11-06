using System;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands
{
    public class GenerateTimeSlotsCommand : IRequest<Result>
    {
        public string BusinessId { get; set; }
        public DateTime Start { get; set; }
    }
}

