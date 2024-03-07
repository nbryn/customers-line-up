using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Messages;

public sealed class BusinessMessage : Message
{
    public override BusinessId SenderId { get; }

    public override UserId ReceiverId { get; }

    private BusinessMessage()
    {
    }

    public BusinessMessage(
        BusinessId senderId,
        UserId receiverId,
        MessageData messageData,
        MessageType type,
        MessageMetadata metadata) : base(senderId, receiverId, messageData, type, metadata)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
    }
}
