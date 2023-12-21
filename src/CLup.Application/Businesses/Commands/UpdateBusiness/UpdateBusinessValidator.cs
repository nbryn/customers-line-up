using CLup.Domain.Businesses.Enums;
using FluentValidation;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessValidator : AbstractValidator<UpdateBusinessCommand>
{
    public UpdateBusinessValidator()
    {
        RuleFor(command => command.Id).NotEmpty();
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Zip).NotEmpty();
        RuleFor(command => command.Street).NotEmpty();
        RuleFor(command => command.City).NotEmpty();
        RuleFor(command => command.Longitude).NotEmpty();
        RuleFor(command => command.Latitude).NotEmpty();
        RuleFor(command => command.Opens).NotEmpty();
        RuleFor(command => command.Closes).NotEmpty();
        RuleFor(command => command.Capacity).NotEmpty();
        RuleFor(command => command.TimeSlotLength).NotEmpty();
        RuleFor(command => command.Type).IsEnumName(typeof(BusinessType));
    }
}
