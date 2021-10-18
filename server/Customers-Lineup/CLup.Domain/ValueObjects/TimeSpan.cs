using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class TimeSpan : ValueObject
    {
        public string Opens { get; private set; }
        public string Closes { get; private set; }

        public TimeSpan() { }

        public TimeSpan(string opens, string closes)
        {
            Opens = opens;
            Closes = closes;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Opens;
            yield return Closes;
        }
    }
}