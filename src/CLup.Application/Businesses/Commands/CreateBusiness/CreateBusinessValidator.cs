using CLup.Domain.Businesses.Enums;
using FluentValidation;

namespace CLup.Application.Businesses.Commands.CreateBusiness;

public sealed class CreateBusinessValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.OwnerId).NotEmpty();
        RuleFor(command => command.Zip).NotEmpty();
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();
        RuleFor(command => command.Opens).NotEmpty();
        RuleFor(command => command.Closes).NotEmpty();
        RuleFor(command => command.Capacity).NotEmpty();
        RuleFor(command => command.TimeSlotLength).NotEmpty();
        RuleFor(command => command.Type).IsEnumName(typeof(BusinessType));
    }
}
