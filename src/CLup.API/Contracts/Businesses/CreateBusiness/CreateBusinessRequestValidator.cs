using FluentValidation;

namespace CLup.API.Contracts.Businesses.CreateBusiness;

public sealed class CreateBusinessRequestValidator : AbstractValidator<CreateBusinessRequest>
{
    public CreateBusinessRequestValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Zip).NotEmpty();
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();

        RuleFor(command => command.OpensAtHour).NotEmpty();
        RuleFor(command => command.OpensAtMinutes).NotEmpty();
        RuleFor(command => command.ClosesAtHour).NotEmpty();
        RuleFor(command => command.ClosesAtMinutes).NotEmpty();

        RuleFor(command => command.Closes).NotEmpty();
        RuleFor(command => command.Capacity).NotEmpty();
        RuleFor(command => command.TimeSlotLength).NotEmpty();
        RuleFor(command => command.Type).NotEmpty().IsInEnum();
        RuleFor(coords => coords.Latitude).NotEmpty().InclusiveBetween(-90, 90);
        RuleFor(coords => coords.Longitude).NotEmpty().InclusiveBetween(-180, 180);
    }
}
