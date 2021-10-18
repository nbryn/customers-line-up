using System;

using CLup.Domain;
using CLup.Domain.ValueObjects;

using TimeSpan = CLup.Domain.ValueObjects.TimeSpan;

namespace CLup.Data.Initializer.DataCreators
{
    public static class BusinessCreator
    {

        public static Business Create(
            string id, string ownerEmail,
            BusinessData businessData, Coords coords, TimeSpan businessHours,
            Address address, double longitude, BusinessType type)
        {
            Business business = new Business
            {
                Id = id,
                OwnerEmail = ownerEmail,
                BusinessData = businessData,
                
                Address = address,
          
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

