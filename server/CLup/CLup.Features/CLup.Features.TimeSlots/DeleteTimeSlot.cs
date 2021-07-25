using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.TimeSlots
{
    public class DeleteTimeSlot
    {
        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly CLupContext _context;
            
            public Handler(CLupContext context) => _context = context;

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {

                return await _context.TimeSlots.FirstOrDefaultAsync(t => t.Id == command.Id)
                        .FailureIf("Time slot not found")
                        .Execute(timeSlot => _context.RemoveAndSave(timeSlot));
            }
        }
    }
}

