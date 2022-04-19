using System.Collections.Generic;

namespace CLup.Domain.Shared.ValueObjects
{
    public class TimeSpan : ValueObject
    {
        public string Start { get; private set; }
        
        public string End { get; private set; }

        public TimeSpan() { }

        public TimeSpan(string start, string end)
        {
            Start = start;
            End = end;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}