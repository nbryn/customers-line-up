using FluentValidation;

namespace CLup.API.Contracts.Businesses.UpdateBusiness;

public sealed class UpdateBusinessRequestValidator : AbstractValidator<UpdateBusinessRequest>
{
    public UpdateBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Zip).NotEmpty();
        RuleFor(request => request.Street).NotEmpty();
        RuleFor(request => request.City).NotEmpty();
        RuleFor(request => request.Opens).NotEmpty();
        RuleFor(request => request.Closes).NotEmpty();
        RuleFor(request => request.Capacity).NotEmpty();
        RuleFor(request => request.TimeSlotLength).NotEmpty();
        RuleFor(command => command.Type).NotEmpty().IsInEnum();
        RuleFor(coords => coords.Latitude).NotEmpty().InclusiveBetween(-90, 90);
        RuleFor(coords => coords.Longitude).NotEmpty().InclusiveBetween(-180, 180);
    }
}
