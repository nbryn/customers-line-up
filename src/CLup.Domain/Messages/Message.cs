using System;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;

namespace CLup.Domain.Messages;

public sealed class Message : Entity<MessageId>
{
    public MessageData MessageData { get; private set; }

    public MessageType Type { get; private set; }

    public Guid SenderId { get; private set; }

    public Guid ReceiverId { get; private set; }

    public MessageMetadata Metadata { get; private set; }

    protected Message()
    {

    }

    public Message(
        Guid senderId,
        Guid receiverId,
        MessageData messageData,
        MessageType type,
        MessageMetadata metadata)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        MessageData = messageData;
        Type = type;
        Metadata = metadata;
        CreatedAt = DateTime.Now;

        Id = MessageId.Create(Guid.NewGuid());
    }

    public Message DeletedBySender()
    {
        Metadata = new MessageMetadata(true);

        return this;
    }

    public Message DeletedByReceiver()
    {
        Metadata = new MessageMetadata(false, true);

        return this;
    }
}
