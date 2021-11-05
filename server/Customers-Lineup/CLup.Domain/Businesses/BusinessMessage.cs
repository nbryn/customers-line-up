using CLup.Domain.Users;
using CLup.Domain.Messages;

namespace CLup.Domain.Businesses
{
    public class BusinessMessage : Message<Business, User>
    {

        protected BusinessMessage() : base(null, null, null, MessageType.Enquiry)
        {

        }

        public BusinessMessage(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type)
            : base(senderId, receiverId, messageData, type)
        {
        }
    }
}