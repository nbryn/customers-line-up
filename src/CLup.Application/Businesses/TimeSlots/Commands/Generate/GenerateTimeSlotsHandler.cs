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
        private readonly ICLupRepository _repository;

        public GenerateTimeSlotsHandler(ICLupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(GenerateTimeSlotsCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.OwnerEmail)
                .FailureIf("User not found.")
                .Ensure(user => user.GetTimeSlotByDate(command.BusinessId, command.Start) == null,
                    "Time slots already generated for this date.", HttpCode.Conflict)
                .AndThen(user => user.GetBusiness(command.BusinessId)?.GenerateTimeSlots(command.Start))
                .Finally(timeSlots => _repository.AddAndSave(timeSlots.ToArray()));
    }
}