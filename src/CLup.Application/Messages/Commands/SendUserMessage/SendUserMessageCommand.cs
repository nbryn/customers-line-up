using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Messages.Commands.SendUserMessage;

public sealed class SendUserMessageCommand : IRequest<Result>
{
    public UserId SenderId { get; }

    public BusinessId ReceiverId { get; }

    public MessageData MessageData { get; }

    public MessageType Type { get; }

    public SendUserMessageCommand(
        UserId senderId,
        BusinessId receiverId,
        MessageData messageData,
        MessageType type)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        MessageData = messageData;
        Type = type;
    }

    public UserMessage MapToUserMessage() =>
        new(SenderId, ReceiverId, MessageData, Type, new MessageMetadata(false, false));
}
