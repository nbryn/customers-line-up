using CLup.Application.Businesses;

namespace CLup.API.Contracts.Businesses;

public readonly record struct GetBusinessesByOwnerResponse(IList<BusinessAggregateDto> Businesses);
