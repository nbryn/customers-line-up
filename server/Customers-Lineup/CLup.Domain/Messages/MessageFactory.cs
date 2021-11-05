using CLup.Domain.Businesses;

namespace CLup.Domain.Messages
{
    public class MessageFactory
    {
        public static BusinessMessage BookingDeletedMessage(Business business, string receiverId)
        {
            var content = $"Your booking at {business.Name} was deleted.";
            var messageData = new MessageData("Booking Deleted", content);
            var message = new BusinessMessage(business.Id, receiverId, messageData, MessageType.BookingDeleted);

            return message;
        }
    }
}