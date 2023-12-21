using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using System;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Shared.Interfaces;

public interface ICLupRepository
{
    Task<Business?> FetchBusinessAggregate(BusinessId businessId);

    Task<User?> FetchUserAggregate(UserId? userId, string? email = null);

    Task<Message?> FetchMessage(MessageId messageId);

    Task<IList<User>> FetchUsersNotEmployedByBusiness(BusinessId businessId);

    Task<IList<Business>> FetchAllBusinesses();

    Task<int> AddAndSave<TId>(params Entity<TId>[] entities);

    Task<int> RemoveAndSave<TId>(Entity<TId> value);

    Task<int> UpdateEntity<T, TId>(Guid id, T updatedEntity) where T : Entity<TId>;
}
