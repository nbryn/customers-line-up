using CLup.Domain.Shared;

namespace CLup.Domain.Businesses;

public static class BusinessErrors
{
    public static Error NotFound => new("Businesses.NotFound", "The business was not found.");

    public static Error InvalidOwner => new("Businesses.InvalidOwner", "You can't update this business.");
}
