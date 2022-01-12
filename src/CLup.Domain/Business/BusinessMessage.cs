using CLup.Domain.Message;

namespace CLup.Domain.Business
{
    public class BusinessMessage : Message<Business, User.User>
    {
        protected BusinessMessage() : base(string.Empty, string.Empty, null, MessageType.Enquiry, null)
        {
        }

        public BusinessMessage(
            string senderId,
            string receiverId,
            MessageData messageData,
            MessageType type,
            MessageMetadata metaData)
            : base(senderId, receiverId, messageData, type, metaData)
        {
        }
    }
}