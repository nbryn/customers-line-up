using CLup.Businesses;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class BusinessCreator
    {

        public static Business Create(
            string name, string ownerEmail,
            string address, string zip, double longitude,
            double latitude, int capacity, string opens,
            string closes, int TimeSlotLength, BusinessType type)
        {
            Business business = new Business
            {
                Name = name,
                OwnerEmail = ownerEmail,
                Zip = zip,
                Address = address,
                Longitude = longitude,
                Latitude = latitude,
                Capacity = capacity,
                Opens = opens,
                Closes = closes,
                TimeSlotLength = TimeSlotLength,
                Type = type
            };

            return business;
        }

        public static BusinessOwner CreateOwner(string userEmail)
        {
            BusinessOwner owner = new BusinessOwner
            {
                UserEmail = userEmail
            };

            return owner;
        }
    }
}

