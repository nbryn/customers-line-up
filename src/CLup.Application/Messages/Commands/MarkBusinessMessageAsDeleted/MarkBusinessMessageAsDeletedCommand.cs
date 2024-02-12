using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.MarkBusinessMessageAsDeleted;

public sealed class MarkBusinessMessageAsDeletedCommand : IRequest<Result>
{
    public UserId RequesterId { get; }

    public MessageId MessageId { get; }

    public BusinessId SenderId { get; }

    public UserId ReceiverId { get; }

    public bool ForSender { get; }

    public MarkBusinessMessageAsDeletedCommand(
        UserId requesterId,
        MessageId messageId,
        BusinessId senderId,
        UserId receiverId,
        bool forSender)
    {
        RequesterId = requesterId;
        ReceiverId = receiverId;
        SenderId = senderId;
        MessageId = messageId;
        ForSender = forSender;
    }
}
