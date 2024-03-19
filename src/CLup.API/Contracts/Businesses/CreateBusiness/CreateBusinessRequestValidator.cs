namespace CLup.API.Contracts.Businesses.CreateBusiness;

public sealed class CreateBusinessRequestValidator : AbstractValidator<CreateBusinessRequest>
{
    public CreateBusinessRequestValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
        RuleFor(request => request.BusinessHours).NotEmpty();
        RuleFor(request => request.Capacity).NotEmpty();
        RuleFor(request => request.TimeSlotLengthInMinutes).NotEmpty();
        RuleFor(request => request.Type).NotEmpty().IsInEnum();
    }
}
