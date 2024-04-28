using CLup.Application.Businesses;

namespace CLup.API.Businesses.Contracts;

public readonly record struct GetBusinessesByOwnerResponse(IList<BusinessAggregateDto> Businesses);
