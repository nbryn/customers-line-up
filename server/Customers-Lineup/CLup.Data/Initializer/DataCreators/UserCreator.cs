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
            return new User(userData, address, coords);
        }
    }
}