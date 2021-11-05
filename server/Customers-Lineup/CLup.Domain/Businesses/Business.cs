using System.Collections.Generic;

using CLup.Domain.Businesses.Employees;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;

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
    }
}