using System;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.Business.TimeSlot.Generate
{
    public class GenerateTimeSlotsCommand : IRequest<Result>
    {
        public string BusinessId { get; set; }
        public DateTime Start { get; set; }
    }
}

