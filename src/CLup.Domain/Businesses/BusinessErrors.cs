using CLup.Domain.Shared;

namespace CLup.Domain.Businesses;

public static class BusinessErrors
{
    public static readonly Error NotFound = new("Business.NotFound", "The business was not found.");
}
