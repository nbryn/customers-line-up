using System.Collections.Generic;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Domain.Businesses
{
    public class Business : Entity
    {

        public string OwnerEmail { get; private set; }

        public BusinessData BusinessData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public TimeSpan BusinessHours { get; private set; }

        public BusinessType Type { get; private set; }

        public IEnumerable<TimeSlot> TimeSlots { get; private set; }

        public IEnumerable<Employee> Employees { get; private set; }

        public IList<Message> SentMessages { get; private set; }

        public IList<Message> ReceivedMessages { get; private set; }

        protected Business()
        {

        }

        public Business(
            string ownerEmail,
            BusinessData businessData,
            Address address,
            Coords coords,
            TimeSpan businessHours,
            BusinessType type)
            : base()
        {
            OwnerEmail = ownerEmail;
            BusinessData = businessData;
            Address = address;
            Coords = coords;
            BusinessHours = businessHours;
            Type = type;
        }

        public string Opens => BusinessHours.Start;

        public string Closes => BusinessHours.End;

        public string Name => BusinessData.Name;
        
        public Message BookingDeletedMessage(string receiverId)
        {
            var content = $"Your booking at {Name} was deleted.";
            var messageData = new MessageData("Booking Deleted", content);
            var metadata = new MessageMetadata(false, false);

            var message = new Message(Id, receiverId, messageData, MessageType.BookingDeleted, metadata);
            return message;
        }
    }
}