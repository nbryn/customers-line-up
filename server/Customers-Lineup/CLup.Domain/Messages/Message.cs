using CLup.Domain.Shared;

namespace CLup.Domain.Messages
{
    public class Message<TSender, TReceiver> : Entity
        where TSender : IHaveMessages
        where TReceiver : IHaveMessages
    {
        public MessageData MessageData { get; private set; }

        public MessageType Type { get; private set; }

        public string SenderId { get; private set; }

        public TSender Sender { get; private set; }

        public string ReceiverId { get; private set; }

        public TReceiver Receiver { get; private set; }

        public Message(
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