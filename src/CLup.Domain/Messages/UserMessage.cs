using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Messages;

public sealed class UserMessage : Message
{
    public new UserId SenderId { get; private set; }

    public new BusinessId ReceiverId { get; private set; }

    protected UserMessage()
    {

    }

    public UserMessage(
        UserId senderId,
        BusinessId receiverId,
        MessageData messageData,
        MessageType type,
        MessageMetadata metadata) : base(senderId, receiverId, messageData, type, metadata)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
    }
}
