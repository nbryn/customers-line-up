using System;
using System.Linq;
using System.Collections.Generic;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Messages;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserData UserData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public Role Role { get; set; }

        public IList<Message> ReceivedMessages { get; private set; } = new List<Message>();

        public IList<Message> SentMessages { get; private set; } = new List<Message>();

        public IList<Business> Businesses { get; private set; } = new List<Business>();

        public IList<Booking> Bookings { get; private set; } = new List<Booking>();

        protected User()
        {
        }

        public User(
            UserData userData,
            Address address,
            Coords coords,
            Role role)
        {
            UserData = userData;
            Address = address;
            Coords = coords;
            Role = role;
        }

        public string Name => UserData.Name;

        public string Email => UserData.Email;

        public string Password => UserData.Password;

        public bool IsBusinessOwner => Businesses?.Count > 0;

        public User UpdateRole(Role role)
        {
            Role = role;

            return this;
        }

        public User Update(string name, string email, (Address address, Coords coords) info)
        {
            UserData = new UserData(name, email, Password);
            Address = info.address;
            Coords = info.coords;

            return this;
        }

        public void UserDeletedBookingMessage(Booking booking, string receiverId)
        {
            var content =
                $"The user with email {Email} deleted her/his booking at {booking.TimeSlot.Start.ToString("dd/MM/yyyy")}.";
            var messageData = new MessageData($"Booking Deleted - {booking.Business.Name}", content);
            var metaData = new MessageMetadata(false, false);

            SentMessages.Add(new Message(Id, receiverId, messageData, MessageType.BookingDeleted, metaData));
        }

        public void BusinessDeletedBookingMessage(Business business, string userId) =>
            business.BookingDeletedMessage(userId);


        public IList<TimeSlot> GenerateTimeSlots(string businessId, DateTime start)
        {
            var business = Businesses?.FirstOrDefault(business => business.Id == businessId);

            return business?.GenerateTimeSlots(start);
        }

        public bool BookingExists(string timeSlotId) => Bookings.Any(booking => booking.TimeSlotId == timeSlotId);
        
        public Booking GetBooking(string bookingId) => Bookings.FirstOrDefault(booking => booking.Id == bookingId);

        public Booking GetBusinessBooking(string businessId, string bookingId) =>
            GetBusiness(businessId)?.Bookings.FirstOrDefault(booking => booking.Id == bookingId);

        public Employee GetEmployee(string businessId, string userId)
            => GetBusiness(businessId)?.Employees.FirstOrDefault(employee => employee.UserId == userId);

        public TimeSlot GetTimeSlotByDate(string businessId, DateTime date)
            => GetBusiness(businessId)?.TimeSlots.FirstOrDefault(timeSlot => timeSlot.Start == date);

        public TimeSlot GetTimeSlot(string timeSlotId)
        {
            var business = Businesses.FirstOrDefault(business =>
                business.TimeSlots.Any(timeSlot => timeSlot.Id == timeSlotId));

            return business?.TimeSlots.FirstOrDefault(timeSlot => timeSlot.Id == timeSlotId);
        }

        public Message GetMessage(string messageId)
            => SentMessages.Concat(ReceivedMessages).FirstOrDefault(message => message.Id == messageId);

        public Business GetBusiness(string businessId) =>
            Businesses.FirstOrDefault(business => business.Id == businessId);
    }
}