using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Businesses;
using CLup.Domain.Messages.Enums;

namespace CLup.Application.Messages.Commands.MarkMessageAsForBusinessDeleted;

public class MarkMessageAsDeletedForBusinessHandler : IRequestHandler<MarkMessageAsDeletedForBusinessCommand, Result>
{
    private readonly ICLupRepository _repository;

    public MarkMessageAsDeletedForBusinessHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(MarkMessageAsDeletedForBusinessCommand command, CancellationToken cancellationToken)
        => await _repository.FetchBusinessAggregate(command.RequesterId, command.SenderId)
            .FailureIfNotFound(BusinessErrors.NotFound)
            .FailureIfNotFound(business => business?.GetMessageById(command.MessageId, command.ReceivedMessage), MessageErrors.NotFound)
            .AndThen(message => message.MarkAsDeleted(command.ReceivedMessage))
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
