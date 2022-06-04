using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands.Generate
{
    public class GenerateTimeSlotsHandler : IRequestHandler<GenerateTimeSlotsCommand, Result>
    {
        private readonly ICLupDbContext _context;

        public GenerateTimeSlotsHandler(ICLupDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GenerateTimeSlotsCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .Ensure(user => user.GetTimeSlotByDate(command.BusinessId, command.Start) == null,
                    (HttpCode.Conflict, "Time slots already generated for this date."))
                .AndThen(user => user.GetBusiness(command.BusinessId)?.GenerateTimeSlots(command.Start))
                .Finally(timeSlots => _context.AddAndSave(timeSlots.ToArray()));
    }
}