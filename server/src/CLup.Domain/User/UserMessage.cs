using CLup.Domain.Message;

namespace CLup.Domain.User
{
    public class UserMessage : Message<User, Business.Business>
    {

        protected UserMessage() : base(null, null, null, MessageType.Enquiry)
        {

        }

        public UserMessage(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type)
            : base(senderId, receiverId, messageData, type)
        {
        }
    }
}