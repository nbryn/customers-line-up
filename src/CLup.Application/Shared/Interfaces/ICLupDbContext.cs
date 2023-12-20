using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Application.Shared.Interfaces;

public interface ICLupRepository
{
    Task<IList<User>> FetchUsersNotEmployedByBusiness(string businessId);

    Task<TimeSlot> FetchTimeSlot(Guid timeSlotId);

    Task<Business> FetchBusiness(Guid businessId);

    Task<User> FetchUserAggregate(Guid userId);

    Task<IList<Business>> FetchAllBusinesses();

    Task<int> AddAndSave<TId>(params Entity<TId>[] entities);

    Task<int> RemoveAndSave<TId>(Entity<TId> value);

    Task<int> UpdateEntity<T, TId>(string id, T updatedEntity) where T : Entity<TId>;
}
