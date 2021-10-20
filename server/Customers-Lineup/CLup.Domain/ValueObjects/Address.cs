using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }

        public string Zip { get; private set; }
        public string City { get; private set; }
        
        public Address() { }

        public Address(string street, string zip, string city)
        {
            Street = street;
            Zip = zip;
            City = city;  
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Zip;
            yield return City;          
        }
    }
}