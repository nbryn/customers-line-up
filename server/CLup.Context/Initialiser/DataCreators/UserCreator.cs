using CLup.Users;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class UserCreator
    {

        public static User Create(string name, string email, string password, string zip, string address, double longitude, double latitude)
        {
            User user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Zip = zip,
                Address = address,
                Longitude = longitude,
                Latitude = latitude
            };

            return user;
        }
    }
}