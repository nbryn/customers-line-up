using CLup.Users;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class UserCreator
    {

        public static User Create(int id, string name, string email, string password, string address, string zip, double longitude, double latitude)
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
                Latitude = latitude
            };

            return user;
        }
    }
}