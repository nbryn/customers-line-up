using CLup.Domain.Message;

namespace CLup.Domain.Business
{
    public class BusinessMessage : Message<Business, User.User>
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