﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CLup.Domain.Businesses;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using System;
using System.Threading;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Shared.Interfaces;

public interface ICLupRepository
{
    Task<User?> FetchUserAggregate(UserId userId);

    Task<User?> FetchUserByEmail(string email);

    Task<Business?> FetchBusinessAggregate(UserId userId, BusinessId businessId);

    Task<Business?> FetchBusinessById(BusinessId businessId);

    Task<IList<Business>> FetchBusinessesByOwner(UserId ownerId);

    Task<IList<User>> FetchUsersNotEmployedByBusiness(BusinessId businessId);

    Task<IList<Business>> FetchAllBusinesses();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<int> AddAndSave(CancellationToken cancellationToken, params Entity[] entities);

    Task<int> RemoveAndSave(Entity value, CancellationToken cancellationToken);

    Task<int> UpdateEntity(Guid id, Entity updatedEntity, CancellationToken cancellationToken);
}
