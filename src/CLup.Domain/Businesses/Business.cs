using System;
using System.Collections.Generic;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Domain.Businesses
{
    public class Business : Entity
    {

        public string OwnerId { get; private set; }

        public User Owner { get; private set; }

        public BusinessData BusinessData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public TimeSpan BusinessHours { get; private set; }

        public BusinessType Type { get; private set; }

        public IEnumerable<Booking> Bookings { get; private set; }

        public IEnumerable<Employee> Employees { get; private set; }

        public IEnumerable<TimeSlot> TimeSlots { get; private set; }

        public IList<Message> SentMessages { get; private set; }

        public IList<Message> ReceivedMessages { get; private set; }

        protected Business()
        {

        }

        public Business(
            string ownerId,
            BusinessData businessData,
            Address address,
            Coords coords,
            TimeSpan businessHours,
            BusinessType type)
            : base()
        {
            OwnerId = ownerId;
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

        public IList<TimeSlot> GenerateTimeSlots(DateTime start)
        {
            var opens = start.AddHours(Double.Parse(Opens.Substring(0, Opens.IndexOf("."))));
            var closes = start.AddHours(Double.Parse(Closes.Substring(0, Closes.IndexOf("."))));

            var timeSlots = new List<TimeSlot>();
            for (var date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(BusinessData.TimeSlotLength))
            {
                var end = date.AddMinutes(BusinessData.TimeSlotLength);
                var timeSlot = new TimeSlot(Id, Name, BusinessData.Capacity, date, end);

                timeSlots.Add(timeSlot);
            }

            return timeSlots;
        }
    }
}