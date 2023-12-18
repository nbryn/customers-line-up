using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.TimeSlots;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;

namespace CLup.Application.Shared.Interfaces
{
    public interface ICLupRepository
    {
        Task<IList<User>> FetchUsersNotEmployedByBusiness(string businessId);
        
        Task<TimeSlot> FetchTimeSlot(string timeSlotId);
        
        Task<Business> FetchBusiness(string businessId);
        
        Task<User> FetchUserAggregate(string mailOrId);
        
        Task<IList<Business>> FetchAllBusinesses();
        
        Task<int> AddAndSave(params Entity[] entities);

        Task<int> RemoveAndSave(Entity value);
        
        Task<int> UpdateEntity<T>(string id, T updatedEntity) where T : Entity;

    }
}