using System;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Messages;

public abstract class Message : Entity
{
    public MessageId Id { get; }

    public Id SenderId { get; private set; }

    public Id ReceiverId { get; private set; }

    public MessageData MessageData { get; private set; }

    public MessageType Type { get; private set; }

    public MessageMetadata Metadata { get; private set; }

    protected Message()
    {
    }

    protected Message(
        Id senderId,
        Id receiverId,
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
