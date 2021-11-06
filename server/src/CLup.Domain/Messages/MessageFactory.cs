using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Users;

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

        public static UserMessage BookingDeletedMessage(Booking booking, string receiverId)
        {
            var content = $"The user with email {booking.User.Email} deleted her/his booking at {booking.TimeSlot.Start.ToString("dd/MM/yyyy")}.";
            var messageData = new MessageData($"Booking Deleted - {booking.Business.Name}", content);
            var message = new UserMessage(booking.User.Id, receiverId, messageData, MessageType.BookingDeleted);

            return message;
        }
    }
}