using CLup.Domain.Businesses;
using CLup.Domain.Messages;

namespace CLup.Domain.Users
{
    public class UserMessage : Message<User, Business>
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