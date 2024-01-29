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
        RuleFor(command => command.Opens).NotEmpty();
        RuleFor(command => command.Longitude).NotEmpty();
        RuleFor(command => command.Latitude).NotEmpty();
        RuleFor(command => command.Closes).NotEmpty();
        RuleFor(command => command.Capacity).NotEmpty();
        RuleFor(command => command.TimeSlotLength).NotEmpty();
        RuleFor(command => command.Type).IsInEnum();
    }
}
