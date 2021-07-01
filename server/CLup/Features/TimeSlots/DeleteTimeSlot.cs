using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

using CLup.Context;
using CLup.Util;

namespace CLup.TimeSlots
{
    public class DeleteTimeSlot
    {
        public class Command : IRequest<Result>
        {

            public string TimeSlotId { get; set; }

            public Command(string timeSlotId) => TimeSlotId = timeSlotId;
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;
            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                var timeSlot = _context.TimeSlots.FirstOrDefault(t => t.Id == command.TimeSlotId);

                if (timeSlot == null)
                {
                    return Result.NotFound();
                }

                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();

                return Result.Deleted();
            }
        }
    }
}

