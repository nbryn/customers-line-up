using System;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands.Generate
{
    public class GenerateTimeSlotsCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }
        
        public string BusinessId { get; set; }

        public DateTime Start { get; set; }
    }
}