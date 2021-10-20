using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class BusinessData : ValueObject
    {
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int TimeSlotLength { get; private set; }

        public BusinessData() { }

        public BusinessData(string name, int capacity, int timeSlotLength)
        {
            Name = name;
            Capacity = capacity;
            TimeSlotLength = timeSlotLength;   
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Name;
            yield return Capacity;
            yield return TimeSlotLength;          
        }
    }
}