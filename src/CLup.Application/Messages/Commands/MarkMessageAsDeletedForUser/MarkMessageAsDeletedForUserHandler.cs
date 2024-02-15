using System;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;
using MediatR;

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
            .AndThen(message => MarkMessageAsDeleted(message, command.ReceivedMessage))
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));

    private Message MarkMessageAsDeleted(Message? message, bool receivedMessage)
        => message switch
        {
            UserMessage userMessage => userMessage.Sender.MarkMessageAsDeleted(userMessage, receivedMessage),
            BusinessMessage businessMessage => businessMessage.Sender.MarkMessageAsDeleted(businessMessage, receivedMessage),
            _ => throw new ArgumentOutOfRangeException(nameof(message), message, null)
        };
}
