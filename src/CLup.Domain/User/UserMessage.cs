using CLup.Domain.Message;

namespace CLup.Domain.User
{
    public class UserMessage : Message<User, Business.Business>
    {

        protected UserMessage() : base(string.Empty, string.Empty, null, MessageType.Enquiry, null)
        {

        }

        public UserMessage(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type,
            MessageMetadata metadata)
            : base(senderId, receiverId, messageData, type, metadata)
        {
        }
    }
}