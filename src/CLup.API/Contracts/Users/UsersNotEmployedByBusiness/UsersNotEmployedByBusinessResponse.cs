using CLup.Application.Users;

namespace CLup.API.Contracts.Users.UsersNotEmployedByBusiness;

public readonly record struct UsersNotEmployedByBusinessResponse(Guid BusinessId, IList<UserDto> Users);
