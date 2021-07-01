using System;
using CLup.Users;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class UserCreator
    {

        public static User Create(
            string id, string name, string email, string password,
            string zip, string address, double longitude, double latitude)
        {
            User user = new User
            {
                Id = id,
                Name = name,
                Email = email,
                Password = password,
                Zip = zip,
                Address = address,
                Longitude = longitude,
                Latitude = latitude,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return user;
        }
    }
}