using CLup.Application.Businesses;

namespace CLup.API.Contracts.Businesses.GetAllBusinesses;

public readonly record struct GetAllBusinessesResponse(IList<BusinessDto> Businesses);
