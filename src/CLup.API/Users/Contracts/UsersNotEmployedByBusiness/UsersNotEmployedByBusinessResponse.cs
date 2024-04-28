using CLup.Application.Users;

namespace CLup.API.Users.Contracts.UsersNotEmployedByBusiness;

public readonly record struct UsersNotEmployedByBusinessResponse(Guid BusinessId, IList<UserDto> Users);
