using System;

using CLup.Domain;
using CLup.Domain.ValueObjects;

namespace CLup.Data.Initializer.DataCreators
{
    public static class UserCreator
    {

        public static User Create(
            string id, UserData userData,
            Address address, Coords coords)
        {
            User user = new User
            {
                Id = id,
                UserData = userData,
                Address = address,
                Coords = coords,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return user;
        }
    }
}