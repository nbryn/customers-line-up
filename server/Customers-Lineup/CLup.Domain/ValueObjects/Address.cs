using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        public Address() { }

        public Address(string street, string zipCode, string city)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;  
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return ZipCode;
            yield return City;          
        }
    }
}