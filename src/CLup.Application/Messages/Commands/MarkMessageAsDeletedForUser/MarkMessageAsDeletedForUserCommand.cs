using CLup.Application.Shared;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Messages.Commands.MarkMessageAsDeletedForUser;

public sealed class MarkMessageAsDeletedForUserCommand : IRequest<Result>
{
    public MessageId MessageId { get; }

    public UserId RequesterId { get; }

    public bool ReceivedMessage { get; }

    public MarkMessageAsDeletedForUserCommand(MessageId messageId, UserId requesterId, bool receivedMessage)
    {
        MessageId = messageId;
        RequesterId = requesterId;
        ReceivedMessage = receivedMessage;
    }
}
