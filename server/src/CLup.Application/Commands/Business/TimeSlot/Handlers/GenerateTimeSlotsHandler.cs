using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Commands.Business.TimeSlot.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.Business.TimeSlot.Handlers
{
    public class GenerateTimeSlotsHandler : IRequestHandler<GenerateTimeSlotsCommand, Result>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public GenerateTimeSlotsHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GenerateTimeSlotsCommand command, CancellationToken cancellationToken)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == command.BusinessId)
                    .FailureIf("Business not found.")
                    .AndThenDouble(() => _context.TimeSlots.FirstOrDefaultAsync(x => x.BusinessId == command.BusinessId && x.Start.Date == command.Start.Date))
                    .Ensure(timeSlot => timeSlot == null, "Time slots already generated for this date.")
                    .AndThen(business => Domain.Business.TimeSlot.TimeSlot.GenerateTimeSlots(business, command.Start))
                    .Finally(timeSlots => _context.AddAndSave(timeSlots.ToArray()));
        }
    }
}