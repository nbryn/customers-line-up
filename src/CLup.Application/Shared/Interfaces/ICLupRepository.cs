using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using System;
using System.Threading;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Shared.Interfaces;

public interface ICLupRepository
{
    Task<Business?> FetchBusinessAggregate(BusinessId businessId);

    Task<User?> FetchUserAggregate(UserId? userId, string? email = null);

    Task<Message?> FetchMessage(MessageId messageId, bool forBusiness);

    Task<IList<User>> FetchUsersNotEmployedByBusiness(BusinessId businessId);

    Task<IList<Business>> FetchAllBusinesses();

    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    Task<int> AddAndSave(params Entity[] entities);

    Task<int> RemoveAndSave(Entity value);

    Task<int> UpdateEntity(Guid id, Entity updatedEntity);
}
