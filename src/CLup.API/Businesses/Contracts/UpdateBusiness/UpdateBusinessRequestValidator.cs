namespace CLup.API.Businesses.Contracts.UpdateBusiness;

public sealed class UpdateBusinessRequestValidator : AbstractValidator<UpdateBusinessRequest>
{
    public UpdateBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
        RuleFor(request => request.Capacity).NotEmpty();
        RuleFor(request => request.TimeSlotLengthInMinutes).NotEmpty();
        RuleFor(command => command.Type).NotEmpty().IsInEnum();
    }
}
