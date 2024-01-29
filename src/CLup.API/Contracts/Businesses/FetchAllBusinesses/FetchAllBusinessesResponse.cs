using CLup.Application.Businesses;

namespace CLup.API.Contracts.Businesses.FetchAllBusinesses;

public readonly record struct FetchAllBusinessesResponse(IList<BusinessDto> Businesses);
