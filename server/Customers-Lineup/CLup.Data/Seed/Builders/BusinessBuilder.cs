using CLup.Domain;
using CLup.Domain.ValueObjects;

using TimeSpan = CLup.Domain.ValueObjects.TimeSpan;

namespace CLup.Data.Seed.Builders
{
    public class BusinessBuilder
    {
        private string _ownerEmail;
        private BusinessData _businessData;
        private Address _address;
        private Coords _coords;
        private TimeSpan _businessHours;
        private BusinessType _type;

        public BusinessBuilder()
        {

        }

        public BusinessBuilder WithOwner(string ownerEmail)
        {
            _ownerEmail = ownerEmail;

            return this;
        }

        public BusinessBuilder WithBusinessData(string name, int capacity, int timeSlotLength)
        {
            _businessData = new BusinessData(name, capacity, timeSlotLength);

            return this;
        }

        public BusinessBuilder WithAddress(string street, string zip, string city)
        {
            _address = new Address(street, zip, city);

            return this;
        }

        public BusinessBuilder WithCoords(double longitude, double latitude)
        {
            _coords = new Coords(longitude, latitude);

            return this;
        }

        public BusinessBuilder WithBusinessHours(string opens, string closes)
        {
            _businessHours = new TimeSpan(opens, closes);

            return this;
        }

        public BusinessBuilder WithType(BusinessType type)
        {
            _type = type;

            return this;
        }

        public Business Build() => new Business(_ownerEmail, _businessData, _address, _coords, _businessHours, _type);
    }
}

