using CLup.Application.Businesses;

namespace CLup.API.Businesses.Contracts;

public readonly record struct GetAllBusinessesResponse(IList<BusinessDto> Businesses);
