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
            return new Business(ownerEmail, businessData, address, coords, businessHours, type);
        }
    }
}

