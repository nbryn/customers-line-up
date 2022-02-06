using System.Collections.Generic;
using CLup.Domain.Bookings;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users
{
    public class User : Entity
    {

        public UserData UserData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public Role Role { get; set; }

        public IList<Booking> Bookings { get; private set; }

        public IList<Message> SentMessages { get; private set; }

        public IList<Message> ReceivedMessages { get; private set; }

        protected User()
        {
        }

        public User(
            UserData userData,
            Address address,
            Coords coords)
            : base()
        {
            UserData = userData;
            Address = address;
            Coords = coords;
        }

        public string Name => UserData.Name;
        public string Email => UserData.Email;
        public string Password => UserData.Password;

        public User Update(string name, string email, (Address address, Coords coords) info)
        {
            UserData = new UserData(name, email, Password);
            Address = info.address;
            Coords = info.coords;

            return this;
        }
        
        public Message BookingDeletedMessage(Booking booking, string receiverId)
        {
            var content =
                $"The user with email {Email} deleted her/his booking at {booking.TimeSlot.Start.ToString("dd/MM/yyyy")}.";
            var messageData = new MessageData($"Booking Deleted - {booking.Business.Name}", content);
            var metaData = new MessageMetadata(false, false);
            
            var message = new Message(Id, receiverId, messageData, MessageType.BookingDeleted, metaData);
            return message;
        }
    }
}