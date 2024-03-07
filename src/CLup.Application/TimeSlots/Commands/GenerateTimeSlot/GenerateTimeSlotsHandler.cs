using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsHandler : IRequestHandler<GenerateTimeSlotsCommand, Result>
{
    private readonly ICLupRepository _repository;

    public GenerateTimeSlotsHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(GenerateTimeSlotsCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.UserId, command.BusinessId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FlatMap(business => business?.GenerateTimeSlots(command.Date), HttpCode.BadRequest)
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
