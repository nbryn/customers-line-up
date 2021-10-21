using System;

using CLup.Domain;
using CLup.Domain.ValueObjects;

using TimeSpan = CLup.Domain.ValueObjects.TimeSpan;

namespace CLup.Data.Initializer.DataCreators
{
    public static class BusinessCreator
    {

        public static Business Create(
            string ownerEmail,
            BusinessData businessData, 
            TimeSpan businessHours,
            Address address, 
            Coords coords, 
            BusinessType type)
        {
            var business = new Business(ownerEmail, businessData, address, coords, businessHours, type);
            business.UpdatedAt = DateTime.Now;

            return business;
        }
    }
}

