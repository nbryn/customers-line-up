using System;

using CLup.Domain;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class BusinessCreator
    {

        public static Business Create(
            string id, string name, string ownerEmail,
            string address, string zip, double longitude,
            double latitude, int capacity, string opens,
            string closes, int TimeSlotLength, BusinessType type)
        {
            Business business = new Business
            {
                Id = id,
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
                Type = type,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return business;
        }

        public static BusinessOwner CreateOwner(string id, string userEmail)
        {
            BusinessOwner owner = new BusinessOwner
            {   
                Id = id,
                UserEmail = userEmail,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return owner;
        }
    }
}

