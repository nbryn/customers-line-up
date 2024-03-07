using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
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

    public Task<int> SaveChangesAsync(bool acceptChanges, CancellationToken cancellationToken = default);

    Task<int> AddAndSave(CancellationToken cancellationToken, params Entity[] entities);
}
