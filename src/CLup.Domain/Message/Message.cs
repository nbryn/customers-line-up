using CLup.Domain.Shared;

namespace CLup.Domain.Message
{
    public abstract class Message<TSender, TReceiver> : Entity
        where TSender : Entity
        where TReceiver : Entity
    {
        public MessageData MessageData { get; private set; }

        public MessageType Type { get; private set; }

        public string SenderId { get; private set; }

        public TSender Sender { get; }

        public string ReceiverId { get; private set; }

        public TReceiver Receiver { get; }

        protected Message(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type)
            : base()
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            MessageData = messageData;
            Type = type;
        }
    }
}