using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Businesses.TimeSlots.Commands.Generate
{
    public class GenerateTimeSlotsHandler : IRequestHandler<GenerateTimeSlotsCommand, Result>
    {
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public GenerateTimeSlotsHandler(ICLupDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GenerateTimeSlotsCommand command, CancellationToken cancellationToken)
            => await _context.Businesses.Include(business => business.Owner)
                .FirstOrDefaultAsync(b => b.Id == command.BusinessId)
                .FailureIf("Business not found.")
                .AndThenDouble(() => _context.TimeSlots.FirstOrDefaultAsync(x =>
                    x.BusinessId == command.BusinessId && x.Start.Date == command.Start.Date))
                .Ensure(timeSlot => timeSlot == null, "Time slots already generated for this date.")
                .AndThen(business => business.Owner.GenerateTimeSlots(business.Id, command.Start))
                .Finally(timeSlots => _context.AddAndSave(timeSlots.ToArray()));
    }
}