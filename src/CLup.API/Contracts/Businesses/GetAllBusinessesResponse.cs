using CLup.Application.Businesses;

namespace CLup.API.Contracts.Businesses;

public readonly record struct GetAllBusinessesResponse(IList<BusinessDto> Businesses);
