using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class UserData : ValueObject
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public UserData() { }

        public UserData(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;        
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Email;
            yield return Password;          
        }
    }
}