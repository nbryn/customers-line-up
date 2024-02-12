using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkUserMessageAsDeleted;

public sealed class MarkUserMessageAsDeletedHandler : IRequestHandler<MarkUserMessageAsDeletedCommand, Result>
{
    private readonly ICLupRepository _repository;

    public MarkUserMessageAsDeletedHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(MarkUserMessageAsDeletedCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.SenderId)
            .FailureIfNotFound(UserErrors.NotFound)
            .FailureIfNotFoundAsync(async user => command.ForSender ?
                user?.GetSentMessageById(command.MessageId)
                : (await _repository.FetchBusinessById(command.ReceiverId))?.GetReceivedMessageById(command.MessageId),
                MessageErrors.NotFound)
            .AndThen(message => message?.Sender.MarkMessageAsDeleted(message, command.ForSender))
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
