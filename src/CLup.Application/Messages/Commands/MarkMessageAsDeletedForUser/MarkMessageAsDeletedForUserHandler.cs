using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeletedForUser;

public sealed class MarkMessageAsDeletedForUserHandler : IRequestHandler<MarkMessageAsDeletedForUserCommand, Result>
{
    private readonly ICLupRepository _repository;

    public MarkMessageAsDeletedForUserHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(MarkMessageAsDeletedForUserCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.RequesterId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFound(user => user?.GetMessageById(command.MessageId, command.ReceivedMessage), MessageErrors.NotFound)
            .AndThen(message => message?.MarkAsDeleted(command.ReceivedMessage))
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));

}
