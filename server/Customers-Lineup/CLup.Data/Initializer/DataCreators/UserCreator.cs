using System;

using CLup.Domain;
using CLup.Domain.ValueObjects;

namespace CLup.Data.Initializer.DataCreators
{
    public static class UserCreator
    {
        public static User Create(
            UserData userData,
            Address address, 
            Coords coords)
        {
            var user = new User(userData, address, coords);
            user.UpdatedAt = DateTime.Now;

            return user;
        }
    }
}