using CLup.Application.Users;

namespace CLup.API.Contracts.Users.FetchUserAggregate;

public readonly record struct FetchUserAggregateResponse(UserDto UserDto);
