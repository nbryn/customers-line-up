using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkMessageAsForBusinessDeleted;

public sealed class MarkMessageAsDeletedForBusinessCommand : IRequest<Result>
{
    public UserId RequesterId { get; }

    public MessageId MessageId { get; }

    public BusinessId SenderId { get; }

    public bool ReceivedMessage { get; }

    public MarkMessageAsDeletedForBusinessCommand(
        UserId requesterId,
        MessageId messageId,
        BusinessId senderId,
        bool receivedMessage)
    {
        RequesterId = requesterId;
        SenderId = senderId;
        MessageId = messageId;
        ReceivedMessage = receivedMessage;
    }
}
