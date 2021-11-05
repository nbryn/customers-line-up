using System.Collections.Generic;

using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Users
{
    public class User : Entity
    {
        public UserData UserData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public Role Role { get; internal set; }

        public IList<Booking> Bookings { get; private set; }

        public IList<UserMessage> SentMessages { get; private set; }

        public IList<BusinessMessage> ReceivedMessages { get; private set; }

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

        public User Update(string email, string name, (Address address, Coords coords) info)
        {
            UserData = new UserData(email, name, Password);
            Address = info.address;
            Coords = info.coords;

            return this;
        }
    }
}