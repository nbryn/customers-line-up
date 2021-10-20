using System.Collections.Generic;

using CLup.Domain.ValueObjects;

namespace CLup.Domain
{
    public class User : BaseEntity
    {
        public UserData UserData { get; private set; }

        public Address Address { get; private set; }

        public Coords Coords { get; private set; }

        public Role Role { get; internal set; }

        public IList<Booking> Bookings { get; private set; }

         public User(
                UserData userData,
                Address address,
                Coords coords)
            : base()
        {
            UserData = userData;
            Address = Address;
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