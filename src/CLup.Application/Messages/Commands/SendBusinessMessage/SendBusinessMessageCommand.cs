using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Messages.Commands.SendBusinessMessage;

public sealed class SendBusinessMessageCommand : IRequest<Result>
{
    public UserId RequesterId { get; }
    public BusinessId SenderId { get; }

    public UserId ReceiverId { get; }

    public MessageData MessageData { get; }

    public MessageType Type { get; }

    public SendBusinessMessageCommand(
        UserId requesterId,
        BusinessId senderId,
        UserId receiverId,
        MessageData messageData,
        MessageType type)
    {
        RequesterId = requesterId;
        SenderId = senderId;
        ReceiverId = receiverId;
        MessageData = messageData;
        Type = type;
    }

    public BusinessMessage MapToBusinessMessage() =>
        new(SenderId, ReceiverId, MessageData, Type, new MessageMetadata(false, false));
}
