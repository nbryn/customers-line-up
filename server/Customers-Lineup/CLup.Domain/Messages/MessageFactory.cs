using CLup.Domain.Businesses;
using CLup.Domain.Users;

namespace CLup.Domain.Messages
{
    public class MessageFactory
    {
        public static Message<Business, User> BookingDeletedMessage(Business business, string receiverId)
        {
            var content = $"Your booking at {business.Name} was deleted.";
            var messageData = new MessageData("Booking Deleted", content);
            var message = new Message<Business, User>(business.Id, receiverId, messageData, MessageType.BookingDeleted);

            return message;
        }
    }
}