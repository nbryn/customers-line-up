using System;
using System.Collections.Generic;
using CLup.Domain.Message;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.User;

using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Domain.Business
{
    public class Business : Entity
    {

        public string OwnerEmail { get; private set; }

        public BusinessData BusinessData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public TimeSpan BusinessHours { get; private set; }

        public BusinessType Type { get; private set; }

        public IEnumerable<TimeSlot.TimeSlot> TimeSlots { get; private set; }

        public IEnumerable<Employee.Employee> Employees { get; private set; }

        public IList<BusinessMessage> SentMessages { get; private set; }

        public IList<UserMessage> ReceivedMessages { get; private set; }

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
        
        public BusinessMessage BookingDeletedMessage(string receiverId)
        {
            var content = $"Your booking at {Name} was deleted.";
            var messageData = new MessageData("Booking Deleted", content);
            var message = new BusinessMessage(Id, receiverId, messageData, MessageType.BookingDeleted);
            message.CreatedAt = DateTime.Now;

            return message;
        }
    }
}