using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Application.Shared.Interfaces;

using System;
using Domain.Businesses.ValueObjects;
using Domain.Users.ValueObjects;

public interface ICLupRepository
{
    Task<IList<User>> FetchUsersNotEmployedByBusiness(BusinessId businessId);

    Task<TimeSlot> FetchTimeSlot(TimeSlotId timeSlotId);

    Task<Business> FetchBusinessAggregate(BusinessId businessId);

    Task<User> FetchUserAggregate(UserId userId);

    Task<IList<Business>> FetchAllBusinesses();

    Task<int> AddAndSave<TId>(params Entity<TId>[] entities);

    Task<int> RemoveAndSave<TId>(Entity<TId> value);

    Task<int> UpdateEntity<T, TId>(Guid id, T updatedEntity) where T : Entity<TId>;
}
