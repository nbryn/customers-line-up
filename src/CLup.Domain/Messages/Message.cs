using System;
using CLup.Domain.Shared;

namespace CLup.Domain.Messages
{
    public class Message : Entity
    {
        public MessageData MessageData { get; private set; }

        public MessageType Type { get; private set; }

        public string SenderId { get; private set; }

        public string ReceiverId { get; private set; }

        public MessageMetadata Metadata { get; private set; }

        protected Message()
        {
            
        }

        public Message(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type,
            MessageMetadata metadata)
            : base()
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            MessageData = messageData;
            Type = type;
            Metadata = metadata;
            CreatedAt = DateTime.Now;
        }

        public Message DeletedBySender()
        {
            Metadata = new MessageMetadata(true, false);

            return this;
        }

        public Message DeletedByReceiver()
        {
            Metadata = new MessageMetadata(false, true);

            return this;
        }
    }
}