using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Messages;

public sealed class UserMessage : Message
{
    public override UserId SenderId { get; }

    public User Sender { get; }

    public override BusinessId ReceiverId { get; }

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
