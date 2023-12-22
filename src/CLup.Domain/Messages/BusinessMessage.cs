using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Messages;

public sealed class BusinessMessage : Message
{
    public new BusinessId SenderId { get; private set; }

    public new UserId ReceiverId { get; private set; }

    protected BusinessMessage()
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
